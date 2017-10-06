using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Services
{
    public interface IStarWarsService
    {
        string GetAllPlanets();
        string GetPlanetById(int planetId);
        string GetResidentsOfPlanet(int planetId);
        string GetAllVehicles();
        string GetPersonDetails(int personId);
    }
}