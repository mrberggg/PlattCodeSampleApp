using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Models
{
    public class ApiPlanetModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("population")]
        public string Population { get; set; }
        [JsonProperty("diameter")]
        public string Diameter { get; set; }
        [JsonProperty("terrain")]
        public string Terrain { get; set; }
        [JsonProperty("orbital_period")]
        public string LengthOfYear { get; set; }
    }
}