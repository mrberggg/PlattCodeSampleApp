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
            _Vehicles = new List<ApiVehicleModel>();
        }
        public List<ApiVehicleModel> _Vehicles { private get; set; }
        
        public List<VehicleStatsViewModel> Details
        {
            get
            {
                var details = new List<VehicleStatsViewModel>();
                _Vehicles.GroupBy(x => x.Manufacturer).ToList()
                    .ForEach(x =>
                    {
                        details.Add(new VehicleStatsViewModel
                        {
                            ManufacturerName = x.First().Manufacturer,
                            VehicleCount = x.Count(),
                            AverageCost = x
                                .Where(y => y.CostInCredits != "unknown")
                                .Average(y => {
                                    long cost;
                                    long.TryParse(y.CostInCredits, out cost);
                                    return cost;
                                })
                        });
                    });
                return details;
            }
            private set { }
        }
        public int VehiclesWithCostCount => _Vehicles.Where(x => x.CostInCredits != null).Count();
        public int ManufacturerCount => _Vehicles.GroupBy(x => x.Manufacturer).Count();
    }
}