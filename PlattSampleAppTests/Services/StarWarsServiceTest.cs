using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlattSampleApp.Services;
using PlattSampleApp.Models;

namespace PlattSampleAppTests.Services
{
    /// <summary>
    /// Summary description for StarWarsServiceTest
    /// </summary>
    [TestClass]
    public class StarWarsServiceTest
    {
        IStarWarsService _starWarsService;
        HttpServiceMock _mockHttpService;

        public StarWarsServiceTest()
        {
            _mockHttpService = new HttpServiceMock();
            _starWarsService = new SWAPIStarWarsService(_mockHttpService);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            _mockHttpService.JsonResponses.Clear();
        }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestGetAllPlanets()
        {
            var responseJson = "{\"results\": [{\"name\": \"Alderaan\", \"rotation_period\": \"24\", \"orbital_period\": \"364\", \"diameter\": \"12500\", \"climate\": \"temperate\",\"gravity\": \"1 standard\",\"terrain\": \"grasslands, mountains\",\"surface_water\": \"40\",\"population\": \"2000000000\",\"residents\": [\"1\",\"2\"],\"films\": [\"1\"]}]}";
            _mockHttpService.JsonResponses["https://swapi.co/api/planets/"] = responseJson;
            var allPlanets = _starWarsService.GetAllPlanets();
            var planet = allPlanets.Find(x => x.Name == "Alderaan");
            // Make sure that all properties are accurately translated to our model
            Assert.AreEqual(planet.Name, "Alderaan");
            Assert.AreEqual(planet.LengthOfDay, "24");
            Assert.AreEqual(planet.LengthOfYear, "364");
            Assert.AreEqual(planet.Diameter, "12500");
            Assert.AreEqual(planet.Climate, "temperate");
            Assert.AreEqual(planet.Gravity, "1 standard");
            Assert.AreEqual(planet.Terrain, "grasslands, mountains");
            Assert.AreEqual(planet.SurfaceWaterPercentage, "40");
            Assert.AreEqual(planet.Population, "2000000000");
            Assert.AreEqual(planet.Residents.Length, 2);
            Assert.AreEqual(planet.Films.Length, 1);
        }

        [TestMethod]
        public void TestGetPlanetById()
        {
            var responseJson = "{\"name\": \"Alderaan\", \"rotation_period\": \"24\", \"orbital_period\": \"364\", \"diameter\": \"12500\", \"climate\": \"temperate\",\"gravity\": \"1 standard\",\"terrain\": \"grasslands, mountains\",\"surface_water\": \"40\",\"population\": \"2000000000\",\"residents\": [\"1\",\"2\"],\"films\": [\"1\"]}";
            var planetId = 1;
            _mockHttpService.JsonResponses[$"https://swapi.co/api/planets/{planetId}"] = responseJson;
            var planet = _starWarsService.GetPlanetById(1);
            // Make sure that all properties are accurately translated to our model
            Assert.AreEqual(planet.Name, "Alderaan");
            Assert.AreEqual(planet.LengthOfDay, "24");
            Assert.AreEqual(planet.LengthOfYear, "364");
            Assert.AreEqual(planet.Diameter, "12500");
            Assert.AreEqual(planet.Climate, "temperate");
            Assert.AreEqual(planet.Gravity, "1 standard");
            Assert.AreEqual(planet.Terrain, "grasslands, mountains");
            Assert.AreEqual(planet.SurfaceWaterPercentage, "40");
            Assert.AreEqual(planet.Population, "2000000000");
            Assert.AreEqual(planet.Residents.Length, 2);
            Assert.AreEqual(planet.Films.Length, 1);
        }

        [TestMethod]
        public void TestGetResidentsOfPlanet()
        {
            var planetName = "Tatooine";
            var resident1Url = "resident1Url";
            var planetsJson = "{\"count\": 1,\"next\":\"\",\"previous\": null,\"results\":[{\"name\": \"" + planetName + "\",\"residents\": [\"" + resident1Url + "\"]}]}";
            var residentJson = "{\"name\": \"Luke Skywalker\", \"height\": \"172\", \"mass\": \"77\", \"hair_color\": \"blond\", \"skin_color\": \"fair\", \"eye_color\": \"blue\", \"birth_year\": \"19BBY\", \"gender\": \"male\", \"homeworld\": \"https://swapi.co/api/planets/1/\", \"films\": [\"https://swapi.co/api/films/2/\"], \"species\": [\"https://swapi.co/api/species/1/\"], \"vehicles\": [\"https://swapi.co/api/vehicles/14/\"], \"starships\": [\"https://swapi.co/api/starships/12/\"]}";
            _mockHttpService.JsonResponses["https://swapi.co/api/planets/"] = planetsJson;
            _mockHttpService.JsonResponses[resident1Url] = residentJson;
            var residents = _starWarsService.GetResidentsOfPlanet(planetName);
            Assert.AreEqual(residents.Count, 1);
            var resident = residents.Find(x => x.Name == "Luke Skywalker");
            // Make sure json is translated to correct attributes
            Assert.AreEqual(resident.Height, "172");
            Assert.AreEqual(resident.Weight, "77");
            Assert.AreEqual(resident.HairColor, "blond");
            Assert.AreEqual(resident.SkinColor, "fair");
            Assert.AreEqual(resident.EyeColor, "blue");
            Assert.AreEqual(resident.BirthYear, "19BBY");
            Assert.AreEqual(resident.Gender, "male");
            Assert.AreEqual(resident.Films.Count, 1);
            Assert.AreEqual(resident.Species.Count, 1);
            Assert.AreEqual(resident.Vehicles.Count, 1);
            Assert.AreEqual(resident.StarShips.Count, 1);
        }

        [TestMethod]
        public void TestGetAllVehicles()
        {
            var vehiclesJson = "{\"count\": 39,\"next\": null,\"previous\": null,\"results\": [{\"name\": \"Sand Crawler\",\"model\": \"Digger Crawler\",\"manufacturer\": \"Corellia Mining Corporation\",\"cost_in_credits\": \"150000\",\"length\": \"36.8\",\"max_atmosphering_speed\": \"30\",\"crew\": \"46\",\"passengers\": \"30\",\"cargo_capacity\": \"50000\",\"consumables\": \"2 months\",\"vehicle_class\": \"wheeled\",\"pilots\": [],\"films\": [\"https://swapi.co/api/films/5/\",\"https://swapi.co/api/films/1/\"]}]}";
            _mockHttpService.JsonResponses["https://swapi.co/api/vehicles/"] = vehiclesJson;
            var vehicles = _starWarsService.GetAllVehicles();
            Assert.AreEqual(vehicles.Count, 1);
            var vehicle = vehicles.Find(x => x.Name == "Sand Crawler");
            Assert.AreEqual(vehicle.Model, "Digger Crawler");
            Assert.AreEqual(vehicle.Manufacturer, "Corellia Mining Corporation");
            Assert.AreEqual(vehicle.CostInCredits, "150000");
            Assert.AreEqual(vehicle.Length, "36.8");
            Assert.AreEqual(vehicle.MaxAtmospheringSpeed, "30");
            Assert.AreEqual(vehicle.Crew, "46");
            Assert.AreEqual(vehicle.Passengers, "30");
            Assert.AreEqual(vehicle.CargoCapacity, "50000");
            Assert.AreEqual(vehicle.Consumables, "2 months");
            Assert.AreEqual(vehicle.VehicleClass, "wheeled");
            Assert.AreEqual(vehicle.Pilots.Count, 0);
            Assert.AreEqual(vehicle.Films.Count, 2);

        }

        [TestMethod]
        public void TestGetPersonByName()
        {
            var homeworldUrl = "testHomeworldUrl";
            var film1Url = "testFilm1Url";
            var species1Url = "testSpecies1Url";
            var vehicle1Url = "testVehicle1Url";
            var starship1Url = "testStarship1Url";
            var peopleJson = "{\"results\":[{\"name\": \"Luke Skywalker\", \"height\": \"172\", \"mass\": \"77\", \"hair_color\": \"blond\", \"skin_color\": \"fair\", \"eye_color\": \"blue\", \"birth_year\": \"19BBY\", \"gender\": \"male\", \"homeworld\": \"" + homeworldUrl + "\", \"films\": [\"" + film1Url + "\"], \"species\": [\"" + species1Url + "\"], \"vehicles\": [\"" + vehicle1Url + "\"], \"starships\": [\"" + starship1Url + "\"]}]}";
            var homeworldJson = "{\"name\": \"Alderaan\", \"rotation_period\": \"24\", \"orbital_period\": \"364\", \"diameter\": \"12500\", \"climate\": \"temperate\",\"gravity\": \"1 standard\",\"terrain\": \"grasslands, mountains\",\"surface_water\": \"40\",\"population\": \"2000000000\",\"residents\": [\"1\",\"2\"],\"films\": [\"1\"]}";
            var film1Json = "{\"title\": \"The Empire Strikes Back\"}";
            var species1Json = "{\"name\": \"Human\"}";
            var vehicle1Json = "{\"name\": \"Snowspeeder\"}";
            var starship1Json = "{\"name\": \"X-wing\"}";
            _mockHttpService.JsonResponses["https://swapi.co/api/people/"] = peopleJson;
            _mockHttpService.JsonResponses[homeworldUrl] = homeworldJson;
            _mockHttpService.JsonResponses[film1Url] = film1Json;
            _mockHttpService.JsonResponses[species1Url] = species1Json;
            _mockHttpService.JsonResponses[vehicle1Url] = vehicle1Json;
            _mockHttpService.JsonResponses[starship1Url] = starship1Json;

            var person = _starWarsService.GetPersonByName("Luke Skywalker");
            // Make sure json is translated to correct attributes
            Assert.AreEqual(person.Height, "172");
            Assert.AreEqual(person.Weight, "77");
            Assert.AreEqual(person.HairColor, "blond");
            Assert.AreEqual(person.SkinColor, "fair");
            Assert.AreEqual(person.EyeColor, "blue");
            Assert.AreEqual(person.BirthYear, "19BBY");
            Assert.AreEqual(person.Gender, "male");
            Assert.AreEqual(person.Homeworld, "Alderaan");
            Assert.AreEqual(person.Films.Count, 1);
            Assert.IsTrue(person.Films.Contains("The Empire Strikes Back"));
            Assert.AreEqual(person.Species.Count, 1);
            Assert.IsTrue(person.Species.Contains("Human"));
            Assert.AreEqual(person.Vehicles.Count, 1);
            Assert.IsTrue(person.Vehicles.Contains("Snowspeeder"));
            Assert.AreEqual(person.StarShips.Count, 1);
            Assert.IsTrue(person.StarShips.Contains("X-wing"));
        }
    }
}
