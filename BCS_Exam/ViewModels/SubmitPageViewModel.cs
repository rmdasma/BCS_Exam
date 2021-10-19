/* 2021 Michael Robin M. Dasmariñas - rmdasma@outlook.com */

using BCS_Exam.Models;
using BCS_Exam.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;

namespace BCS_Exam.ViewModels
{
    public class SubmitPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ICustomerService _customerService;
        private readonly IPageDialogService _pageDialogService;
        public DelegateCommand SubmitCommand => new DelegateCommand(async () => await Submit());

        public string Email { get; set; }
        public CustomerDTO SelectedCustomer { get; set; }

        private bool _hasEmail = true;
        public bool HasEmail
        {
            get => _hasEmail;
            set => SetProperty(ref _hasEmail, value);
        }

        public SubmitPageViewModel(INavigationService navigationService, ICustomerService customerService, IPageDialogService pageDialogService)
        : base(navigationService)
        {
            _navigationService = navigationService;
            _customerService = customerService;
            _pageDialogService = pageDialogService;
            Title = "Submit";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var model = parameters.GetValue<CustomerDTO>("model");
            if (model != null)
                SelectedCustomer = model;
        }

        private async Task Submit()
        {
            HasEmail = !string.IsNullOrEmpty(Email);
            if (!HasEmail)
                return;

            try
            {
                var results = await _customerService.SubmitResponse(SelectedCustomer.ReservationId, Email);
                if (results)
                    await _navigationService.GoBackAsync();
            }
            catch (Exception ex)
            {
                _ = _pageDialogService.DisplayAlertAsync("Error", ex.Message, "Ok");
            }
    
        }
    }
}