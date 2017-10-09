using Newtonsoft.Json;
using PlattSampleApp.Exceptions;
using PlattSampleApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace PlattSampleApp.Services
{
    public class SWAPIStarWarsService : IStarWarsService
    {
        private IHttpService _httpService;

        // Note: I've put our http calls in its own service but am not using any 3rd party libraries
        public SWAPIStarWarsService(IHttpService httpService)
        {
            _httpService = httpService;
        }
      
        public List<ApiPlanetModel> GetAllPlanets()
        {
            var listOfPlanets = new List<ApiPlanetModel>();
            string nextUrl = "https://swapi.co/api/planets/";
            // The api returns a paginated response. 
            // To get all items, loop through until the next property is null
            while (true)
            {
                var response = _httpService.Get<ApiResponseModel<ApiPlanetModel>>(nextUrl);
                listOfPlanets.AddRange(response.Results);
                nextUrl = response.Next;
                // Break when no next url found
                if (nextUrl == null) break;
            }

            return listOfPlanets;
        }

        public List<ApiVehicleModel> GetAllVehicles()
        {
            var vehicles = new List<ApiVehicleModel>();
            string nextUrl = "https://swapi.co/api/vehicles/";
            // The api returns a paginated response. 
            // To get all items, loop through until the next property is null
            while (true)
            {
                var response = _httpService.Get<ApiResponseModel<ApiVehicleModel>>(nextUrl);
                vehicles.AddRange(response.Results);
                nextUrl = response.Next;
                // Break when no next url found
                if (nextUrl == null) break;
            }

            return vehicles;
        }

        public ApiPersonModel GetPersonByName(string personName)
        {
            ApiPersonModel person = null;
            string nextUrl = "https://swapi.co/api/people/";
            // The api returns a paginated response. 
            // To get all items, loop through until the next property is null
            while (true)
            {
                var response = _httpService.Get<ApiResponseModel<ApiPersonModel>>(nextUrl);
                // Check to see if planet is in results
                if (response.Results.Exists(x => x.Name == personName))
                {
                    person = response.Results.Find(x => x.Name == personName);
                    break;
                }
                // If not, continue search
                nextUrl = response.Next;
                // Break when no next url found
                if (nextUrl == null) break;
            }
            // Make sure person exists
            if (person == null)
            {
                throw new PlanetNotFoundException();
            }
            // Get homeworld
            person.Homeworld = _httpService.Get<ApiPlanetModel>(person.Homeworld).Name;
            // Get films
            var films = new List<string>();
            foreach(var film in person.Films)
            {
                films.Add(_httpService.Get<ApiFilmModel>(film).Title);
            }
            person.Films = films;
            // Get species
            var species = new List<string>();
            foreach(var s in person.Species)
            {
                species.Add(_httpService.Get<ApiSpeciesModel>(s).Name);
            }
            person.Species = species;
            // Get vehicles
            var vehicles = new List<string>();
            foreach(var vehicle in person.Vehicles)
            {
                vehicles.Add(_httpService.Get<ApiVehicleModel>(vehicle).Name);
            }
            person.Vehicles = vehicles;
            // Get starships
            var starships = new List<string>();
            foreach(var starship in person.StarShips)
            {
                starships.Add(_httpService.Get<ApiStarshipModel>(starship).Name);
            }
            person.StarShips = starships;
            
            return person;
        }

        public ApiPlanetModel GetPlanetById(int planetId)
        {
            var allPlanets = new List<ApiPlanetModel>();

            string url = $"https://swapi.co/api/planets/{planetId}";
            var planet = _httpService.Get<ApiPlanetModel>(url);
            planet.Id = planetId;
            return planet;
        }

        public List<ApiPersonModel> GetResidentsOfPlanet(string planetName)
        {
            ApiPlanetModel planet = null;
            string nextUrl = "https://swapi.co/api/planets/";
            // The api returns a paginated response. 
            // To get all items, loop through until the next property is null
            while (true)
            {
                var response = _httpService.Get<ApiResponseModel<ApiPlanetModel>>(nextUrl);
                // Check to see if planet is in results
                if(response.Results.Exists(x => x.Name == planetName))
                {
                    planet = response.Results.Find(x => x.Name == planetName);
                    break;
                }
                // If not, continue search
                nextUrl = response.Next;
                // Break when no next url found
                if (nextUrl == null) break;
            }
            // Make sure planet exists
            if (planet == null)
            {
                throw new PlanetNotFoundException();
            }
                
            var residents = new List<ApiPersonModel>();
            for(var i = 0; i < planet.Residents.Length; i++)
            {
                var resident = _httpService.Get<ApiPersonModel>(planet.Residents[i]);
                residents.Add(resident);
            }

            return residents;
        }
    }
}