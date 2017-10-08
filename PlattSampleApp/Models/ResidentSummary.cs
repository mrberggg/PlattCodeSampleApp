using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Models
{
    public class ResidentSummary
    {
        public string Name { get; set; }

        public string Height { get; set; }

        public string Weight { get; set; }

        public string Gender { get; set; }

        public string HairColor { get; set; }

        public string EyeColor { get; set; }

        public string SkinColor { get; set; }

        public string FormattedHeight => Height == "unknown" ? "Unknown" : $"{Height}cm";

        public string FormattedWeight => Weight == "unknown" ? "Unknown" : $"{Weight}kg";
    }
}