using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlattSampleApp.Models;
using PlattSampleApp.Services;

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
            var model = new AllPlanetsViewModel();
            // TODO: Implement this controller action

            return View(model);
        }

        public ActionResult GetPlanetTwentyTwo(int planetid)
        {
            var model = new SinglePlanetViewModel();

            // TODO: Implement this controller action

            return View(model);
        }

        public ActionResult GetResidentsOfPlanetNaboo(string planetname)
        {
            var model = new PlanetResidentsViewModel();

            // TODO: Implement this controller action

            return View(model);
        }

        public ActionResult VehicleSummary()
        {
            var model = new VehicleSummaryViewModel();

            // TODO: Implement this controller action

            return View(model);
        }
    }
}