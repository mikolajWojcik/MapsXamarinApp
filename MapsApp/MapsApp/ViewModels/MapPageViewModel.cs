using MapsApp.Models;
using MapsApp.Services.Interfaces;
using MapsApp.Views;
using Microsoft.AppCenter.Crashes;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MapsApp.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private bool _isInternetAccessible;
        private bool _arePermissionsSatisfied;
        private readonly IPageDialogService _pageDialog;

        public MapPageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IPlacesStorage placesStorage)
            : base(navigationService)
        {
            _pageDialog = pageDialog;

            Title = "Map";         
            PlacesStorage = placesStorage;
            SearchAddressesCommand = new DelegateCommand(SearchAddresses, CanNavigateToAddressesPage);
            IsInternetAccessible = Connectivity.NetworkAccess == NetworkAccess.Internet;

            Connectivity.ConnectivityChanged += (sender, e) =>
            {
                IsInternetAccessible = e.NetworkAccess == NetworkAccess.Internet;
                SearchAddressesCommand.RaiseCanExecuteChanged();
            };
        }

        public bool ArePermissionsSatisfied
        {
            get { return _arePermissionsSatisfied; }
            set { SetProperty(ref _arePermissionsSatisfied, value); }
        }

        public bool IsInternetAccessible
        {
            get { return _isInternetAccessible; }
            set { SetProperty(ref _isInternetAccessible, value); }
        }

        public DelegateCommand SearchAddressesCommand { get; private set; }

        public IPlacesStorage PlacesStorage { get; }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            await PlacesStorage.Initialize;

            ArePermissionsSatisfied = await CheckPermissions();
        }

        private async Task<bool> CheckPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _pageDialog.DisplayAlertAsync("Need location", "Application needs information about mobile location to show it on map", "OK");
                        });
                    }

                    var permissionsDictionary = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = permissionsDictionary[Permission.Location];
                }

                return status == PermissionStatus.Granted;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return false;
            }
        }

        private bool CanNavigateToAddressesPage()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        private void SearchAddresses()
        {
            Device.BeginInvokeOnMainThread( async () =>
            {
                var result = await NavigationService.NavigateAsync(nameof(AddressesPage));

                if (!result.Success)
                    Crashes.TrackError(result.Exception);
            });
        }
    }
}
