using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapsApp.ViewModels
{
	public class AddressesPageViewModel : ViewModelBase
	{
        public AddressesPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
	}
}
