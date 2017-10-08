using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Models
{
    public class VehicleSummaryViewModel
    {
        public VehicleSummaryViewModel()
        {
            Details = new List<VehicleStatsViewModel>();
        }
        
        public List<VehicleStatsViewModel> Details { get; set; }

        public int VehiclesWithCostCount { get; set; }

        public int ManufacturerCount { get; set; }
    }
}