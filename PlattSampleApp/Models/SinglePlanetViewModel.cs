﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Models
{
    public class SinglePlanetViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LengthOfYear { get; set; }

        public string LengthOfDay { get; set; }

        public string Diameter { get; set; }

        public string Climate { get; set; }

        public string Gravity { get; set; }

        public string SurfaceWaterPercentage { get; set; }

        public string Population { get; set; }
    }
}