using MapsApp.Models;
using MapsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapsApp.Services
{
    public class SettingsService : ISettingsService
    {
        public Task<IEnumerable<Place>> LoadPlacesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
