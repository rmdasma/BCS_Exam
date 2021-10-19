/* 2021 Michael Robin M. Dasmariñas - rmdasma@outlook.com */

using Prism.Mvvm;
using Prism.Navigation;

namespace BCS_Exam.ViewModels
{
    public abstract class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        // INavigationAware
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        // INavigationAware
        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        // IDestructible
        public virtual void Destroy()
        {

        }
    }
}