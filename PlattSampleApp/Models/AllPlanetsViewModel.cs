﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Models
{
    public class AllPlanetsViewModel
    {
        public AllPlanetsViewModel()
        {
            Planets = new List<PlanetDetailsViewModel>();
        }

        public List<PlanetDetailsViewModel> Planets { get; set; }

        public double AverageDiameter => Planets
            .Where(p => p.Diameter != 0)
            .Select(p => p.Diameter)
            .Average();
    }
}
