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
                var vehiclesGroupedByManufacturer = _Vehicles.GroupBy(x => x.Manufacturer)
                    .ToList();
                vehiclesGroupedByManufacturer.ForEach(x =>
                {
                    double averageCost = 0;
                    if(x.Where(y => y.CostInCredits != "unknown").ToList().Count() > 0)
                    {
                        averageCost = x.Where(y => y.CostInCredits != "unknown").Select(y =>
                        {
                            return (double) long.Parse(y.CostInCredits);
                        })
                        .Where(y => y != 0)
                        .Average();
                    }
                    details.Add(new VehicleStatsViewModel
                    {
                        ManufacturerName = x.First().Manufacturer,
                        VehicleCount = x.Count(),
                        AverageCost = averageCost
                    });
                });
                details = details
                    .OrderByDescending(x => x.VehicleCount)
                    .OrderByDescending(x => x.AverageCost)
                    .ToList();
                return details;
            }
            private set { }
        }
        public int VehiclesWithCostCount => _Vehicles.Where(x => x.CostInCredits != null).Count();
        public int ManufacturerCount => _Vehicles.GroupBy(x => x.Manufacturer).Count();
    }
}