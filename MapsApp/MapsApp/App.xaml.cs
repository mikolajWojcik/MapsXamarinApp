using Prism;
using Prism.Ioc;
using MapsApp.ViewModels;
using MapsApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using MapsApp.Services;
using MapsApp.Services.Interfaces;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MapsApp
{
    public partial class App
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
            RegisterServices(containerRegistry);
            RegisterViewsForNavigation(containerRegistry);
        }

        private void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IGooglePlacesService, GooglePlacesService>();
            containerRegistry.RegisterSingleton<ISettingsService, SettingsService>();
        }

        private static void RegisterViewsForNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
            containerRegistry.RegisterForNavigation<AddressesPage, AddressesPageViewModel>();
        }
    }
}
