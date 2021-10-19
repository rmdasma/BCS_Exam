/* 2021 Michael Robin M. Dasmariñas - rmdasma@outlook.com */

using BCS_Exam.Models;
using BCS_Exam.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BCS_Exam.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ICustomerService _customerService;
        private readonly IPageDialogService _pageDialogService;
        public DelegateCommand SearchCommand => new DelegateCommand(async () => await SearchCustomers());
        public DelegateCommand SelectCustomerCommand => new DelegateCommand(async () => await SelectCustomer());

        public string ParkCode { get; set; }
        public DateTime Arrival { get; set; }
        public CustomerDTO SelectedCustomer { get; set; }
        private bool _hasCustomers;
        public bool HasCustomers
        {
            get => _hasCustomers;
            set => SetProperty(ref _hasCustomers, value);
        }

        private bool _hasParkCode = true;
        public bool HasParkCode
        {
            get => _hasParkCode;
            set => SetProperty(ref _hasParkCode, value);
        }

        private bool _hasArrivalDate = true;
        public bool HasArrivalDate
        {
            get => _hasArrivalDate;
            set => SetProperty(ref _hasArrivalDate, value);
        }

        public ObservableCollection<CustomerDTO> Customers { get; private set; }
        public MainPageViewModel(INavigationService navigationService, ICustomerService customerService, IPageDialogService pageDialogService)
        : base(navigationService)
        {
            _navigationService = navigationService;
            _customerService = customerService;
            _pageDialogService = pageDialogService;
            Customers = new ObservableCollection<CustomerDTO>();
            Arrival = DateTime.Now; //Set default arrival date to now.
            Title = "Search";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            //If there are existing customers call SearchCustomers to update list and remove processed items.
            if (Customers.Count > 0)
                _ = SearchCustomers();
        }

        private bool ValidateInput()
        {
            var result = true;
            HasParkCode = !string.IsNullOrEmpty(ParkCode);
            if (!HasParkCode)
                result = false;

            HasArrivalDate = !string.IsNullOrEmpty(Arrival.ToString("yyyy-MM-dd"));
            if (!HasArrivalDate)
                result = false;
            return result;
        }

        private async Task SearchCustomers()
        {
            if (!ValidateInput())
                return;

            try
            {
                var results = await _customerService.GetCustomers(ParkCode, Arrival.ToString("yyyy-MM-dd"));
                HasCustomers = results != null && results.Count > 0;
                if (HasCustomers)
                {
                    Customers.Clear();
                    results.ForEach(c => Customers.Add(c));
                }
            }
            catch (Exception ex)
            {
                _ = _pageDialogService.DisplayAlertAsync("Error", ex.Message, "Ok");
            }
        }

        private async Task SelectCustomer()
        {
            if (SelectedCustomer != null)
            {
                var navigationParams = new NavigationParameters
                {
                    { "model", SelectedCustomer }
                };
                await _navigationService.NavigateAsync("SubmitPage", navigationParams);
            }
        }
    }
}