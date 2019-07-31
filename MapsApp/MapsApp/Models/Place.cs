using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapsApp.Models
{
    public class Place
    {
        [JsonProperty("formatted_address")]
        public string FormattedAddres { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
