/* 2021 Michael Robin M. Dasmariñas - rmdasma@outlook.com */

using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using BCS_Exam.Views;
using BCS_Exam.ViewModels;
using BCS_Exam.Services;

namespace BCS_Exam
{
    public partial class App : PrismApplication
    {
        /*
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor.
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SubmitPage, SubmitPageViewModel>();

            //Services
            containerRegistry.RegisterSingleton(typeof(ICustomerService), typeof(CustomerService));
        }
    }
}