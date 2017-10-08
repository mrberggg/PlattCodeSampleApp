using PlattSampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Services
{
    public interface IStarWarsService
    {
        List<ApiPlanetModel> GetAllPlanets();
        ApiPlanetModel GetPlanetById(int planetId);
        List<ApiPersonModel> GetResidentsOfPlanet(string planetName);
        List<ApiVehicleModel> GetAllVehicles();
        ApiPersonModel GetPersonByName(string personName);
    }
}