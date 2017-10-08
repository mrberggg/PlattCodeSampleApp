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

        public string[] Films { get; set; }

        public string[] Species { get; set; }

        public string[] Vehicles { get; set; }

        public string[] StarShips { get; set; }

        public string FormattedFilms => Films.Length > 0 ? String.Join(", ", Films) : "-";

        public string FormattedSpecies => Species.Length > 0 ? String.Join(", ", Species) : "-";

        public string FormattedVehicles => Vehicles.Length > 0 ? String.Join(", ", Vehicles) : "-";

        public string FormattedStarships => StarShips.Length > 0 ? String.Join(", ", StarShips) : "-";
    }
}