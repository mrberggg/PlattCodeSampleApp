using AutoMapper;
using Microsoft.Owin;
using Owin;
using PlattSampleApp.Models;
using System.Collections.Generic;

[assembly: OwinStartupAttribute(typeof(PlattSampleApp.Startup))]
namespace PlattSampleApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<ApiPlanetModel, PlanetDetailsViewModel>()
                    .ConvertUsing(src =>
                    {
                        int diameter;
                        int.TryParse(src.Diameter, out diameter);
                        return new PlanetDetailsViewModel
                        {
                            Name = src.Name,
                            Population = src.Population,
                            Diameter = diameter,
                            Terrain = src.Terrain,
                            LengthOfYear = src.LengthOfYear
                        };
                    });
            });
        }
    }
}
