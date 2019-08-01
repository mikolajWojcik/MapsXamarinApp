using MapsApp.Models;
using MapsApp.Services.Interfaces;
using MapsApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace MapsApp.ViewModels
{
	public class AddressesPageViewModel : ViewModelBase
	{
        private readonly IPlacesStorage _storage;
        private ObservableCollection<Place> _places;
        private bool _isLableVisible;

        public AddressesPageViewModel(INavigationService navigationService, IGooglePlacesService placesService, IPlacesStorage storage) 
            : base(navigationService)
        {
            Title = "Locations";

            PlacesService = placesService;
            _storage = storage;

            SearchAddressCommand = new DelegateCommand<string>(SearchTextAsync, (t) => !string.IsNullOrWhiteSpace(t));
            PlaceSelectedCommand = new DelegateCommand<Place>(SelectedPlace, (t) => t != null);
        }

        public bool IsLableVisible
        {
            get { return _isLableVisible; }
            set { SetProperty(ref _isLableVisible, value); }
        }

        public ObservableCollection<Place> Places
        {
            get { return _places; }
            set { SetProperty(ref _places, value); }
        }

        public IGooglePlacesService PlacesService { get; }
        public DelegateCommand<string> SearchAddressCommand { get; }
        public DelegateCommand<Place> PlaceSelectedCommand { get; }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            Places = new ObservableCollection<Place>();
        }

        private async void SearchTextAsync(string text)
        {
            Places = new ObservableCollection<Place>(await PlacesService.GetPlacesAsync(text));
            IsLableVisible = Places.Count == 0;
        }

        private async void SelectedPlace(Place place)
        {
            await _storage.AddPlaceToCollectionAsync(place);

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await NavigationService.GoBackAsync();
            });
        }
    }
}
