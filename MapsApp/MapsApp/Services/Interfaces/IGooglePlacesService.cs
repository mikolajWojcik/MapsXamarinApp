using MapsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapsApp.Services.Interfaces
{
    public interface IGooglePlacesService
    {
        Task<IEnumerable<Place>> GetPlacesAsync(string name);
        Task<IEnumerable<Place>> LoadMorePlacesAsync();
    }
}
