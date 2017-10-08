using AutoMapper;
using Microsoft.Owin;
using Owin;
using PlattSampleApp.Models;
using System.Collections.Generic;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(PlattSampleApp.Startup))]
namespace PlattSampleApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<ApiPersonModel, PersonViewModel>();
                config.CreateMap<ApiPersonModel, ResidentSummary>();
                config.CreateMap<ApiPlanetModel, PlanetDetailsViewModel>()
                    .ForMember(dest => dest.Diameter, opt => opt.ResolveUsing(p =>
                    {
                        int diameter = 0;
                        if (p.Diameter != "unknown")
                        {
                            diameter = int.Parse(p.Diameter);
                        }
                        return diameter;
                    }));
                config.CreateMap<ApiPlanetModel, SinglePlanetViewModel>();
                config.CreateMap<List<ApiVehicleModel>, VehicleSummaryViewModel>()
                    .ConvertUsing(src =>
                    {
                        var details = src.GroupBy(x => x.Manufacturer).Select(x =>
                            {
                                double averageCost = 0;
                                if (x.Where(y => y.CostInCredits != "unknown").Count() > 0)
                                {
                                    averageCost = x.Where(y => y.CostInCredits != "unknown")
                                        .Select(y =>  (double)long.Parse(y.CostInCredits))
                                        .Where(y => y != 0)
                                        .Average();
                                }
                                return new VehicleStatsViewModel
                                {
                                    AverageCost = averageCost,
                                    ManufacturerName = x.First().Manufacturer,
                                    VehicleCount = x.Count()
                                };
                            })
                            .OrderByDescending(x => x.VehicleCount)
                            .OrderByDescending(x => x.AverageCost)
                            .ToList();
                        return new VehicleSummaryViewModel
                        {
                            Details = details,
                            ManufacturerCount = src.GroupBy(x => x.Manufacturer).Count(),
                            VehiclesWithCostCount = src.Where(x => x.CostInCredits != null).Count()
                        };
                    });         
            });
        }
    }
}
