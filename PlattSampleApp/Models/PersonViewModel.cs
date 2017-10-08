using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Models
{
    public class PersonViewModel
    {
        public string Name { get; set; }

        public string Height { get; set; }

        public string Weight { get; set; }

        public string HairColor { get; set; }

        public string SkinColor { get; set; }

        public string EyeColor { get; set; }

        public string BirthYear { get; set; }

        public string Gender { get; set; }

        public string Homeworld { get; set; }

        public List<string> Films { get; set; }

        public List<string> Species { get; set; }

        public List<string> Vehicles { get; set; }

        public List<String> StarShips { get; set; }

        public string FormattedWeight => Weight == "unknown" ? "Unknown" : $"{Weight}kg";

        public string FormattedFilms => Films.Count > 0 ? String.Join(", ", Films) : "-";

        public string FormattedSpecies => Species.Count > 0 ? String.Join(", ", Species) : "-";

        public string FormattedVehicles => Vehicles.Count > 0 ? String.Join(", ", Vehicles) : "-";

        public string FormattedStarships => StarShips.Count > 0 ? String.Join(", ", StarShips) : "-";
    }
}