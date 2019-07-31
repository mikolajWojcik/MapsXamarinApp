using MapsApp.Models;
using MapsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapsApp.Services
{
    public class GooglePlacesService : IGooglePlacesService
    {
        public Task<IEnumerable<Place>> GetPlacesAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Place>> LoadMorePlacesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
