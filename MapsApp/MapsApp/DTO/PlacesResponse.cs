using MapsApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MapsApp.DTO
{
    public class PlacesResponse
    {
        [JsonProperty("next_page_token")]
        public string NextPageToken { get; set; }

        [JsonProperty("results")]
        public IEnumerable<Place> Places { get; set; } 

        [JsonProperty("status"), JsonConverter(typeof(StringEnumConverter))]
        public HttpStatusCode Status { get; set; }
    }
}
