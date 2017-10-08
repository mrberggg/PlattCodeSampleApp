using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlattSampleApp.Models;
using PlattSampleApp.Services;
using AutoMapper;

namespace PlattSampleApp.Controllers
{
    public class HomeController : Controller
    {
        private IStarWarsService _starWarsService;

        public HomeController(IStarWarsService starWarsService)
        {
            _starWarsService = starWarsService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllPlanets()
        {
            var allPlanets = _starWarsService.GetAllPlanets();
            var formattedPlanets = Mapper.Map<List<PlanetDetailsViewModel>>(allPlanets)
                    .OrderByDescending(x => x.Diameter).ToList();

            return View(new AllPlanetsViewModel
            {
                Planets = formattedPlanets
            });
        }

        public ActionResult GetPlanet(int planetId)
        {
            var planet = _starWarsService.GetPlanetById(planetId);
            var model = Mapper.Map<SinglePlanetViewModel>(planet);
            
            return View(model);
        }

        public ActionResult GetResidentsOfPlanet(string planetName)
        {
            var residents = _starWarsService.GetResidentsOfPlanet(planetName);
            var model = Mapper.Map<List<ResidentSummary>>(residents)
                .OrderBy(x => x.Name).ToList();
            
            return View(model);
        }

        public ActionResult VehicleSummary()
        {
            var vehicles = _starWarsService.GetAllVehicles();
            var model = Mapper.Map<VehicleSummaryViewModel>(vehicles);

            return View(model);
        }

        public ActionResult GetPersonDetails(string personName)
        {
            var person = _starWarsService.GetPersonByName(personName);
            var model = Mapper.Map<PersonViewModel>(person);

            return View(model);
        }
    }
}