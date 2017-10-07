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

            var detailsViewModel = Mapper.Map<List<PlanetDetailsViewModel>>(allPlanets)
                .OrderByDescending(x => x.Diameter).ToList();
            var model = new AllPlanetsViewModel
            {
                Planets = detailsViewModel
            };

            return View(model);
        }

        public ActionResult GetPlanet(int planetId)
        {
            var planet = _starWarsService.GetPlanetById(planetId);
            var model = Mapper.Map<SinglePlanetViewModel>(planet);
            model.Id = planetId;
            
            return View(model);
        }

        public ActionResult GetResidentsOfPlanet(int planetId)
        {
            var residents = _starWarsService.GetResidentsOfPlanet(planetId);
            var model = Mapper.Map<List<ResidentSummary>>(residents);
            
            return View(model);
        }

        public ActionResult VehicleSummary()
        {
            var vehicles = _starWarsService.GetAllVehicles();

            var model = new VehicleSummaryViewModel
            {
                _Vehicles = vehicles
            };

            return View(model);
        }
    }
}