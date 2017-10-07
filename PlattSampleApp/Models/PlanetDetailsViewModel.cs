using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Models
{
    public class PlanetDetailsViewModel
    {
        public string Name { get; set; }
        public string Population { get; set; }
        public int Diameter { get; set; }
        public string Terrain { get; set; }
        public string LengthOfDay { get; set; }
        public string LengthOfYear { get; set; }
        public string FormattedDiameter => Diameter == 0 ? "Unknown" : Diameter.ToString();
        public string FormattedPopulation => Population == "unknown" ? "Unknown" : long.Parse(Population).ToString("N0");
    }
}