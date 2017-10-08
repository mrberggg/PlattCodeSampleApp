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
      
        private T getResultForRoute<T>(string url)
        {
            var result = "";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            
            using (var reader = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

        public List<ApiPlanetModel> GetAllPlanets()
        {
            var listOfPlanets = new List<ApiPlanetModel>();
            string nextUrl = "https://swapi.co/api/planets/?format=json";
            // The api returns a paginated response. 
            // To get all items, loop through until the next property is null
            while (true)
            {
                var response = getResultForRoute<ApiResponseModel<ApiPlanetModel>>(nextUrl);
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
            string nextUrl = "https://swapi.co/api/vehicles/?format=json";
            // The api returns a paginated response. 
            // To get all items, loop through until the next property is null
            while (true)
            {
                var response = getResultForRoute<ApiResponseModel<ApiVehicleModel>>(nextUrl);
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
            string nextUrl = "https://swapi.co/api/people/?format=json";
            // The api returns a paginated response. 
            // To get all items, loop through until the next property is null
            while (true)
            {
                var response = getResultForRoute<ApiResponseModel<ApiPersonModel>>(nextUrl);
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
            person.Homeworld = getResultForRoute<ApiPlanetModel>(person.Homeworld).Name;
            // Get films
            string[] films = new string[person.Films.Length];
            for(var i = 0; i < person.Films.Length; i++)
            {
                films[i] = getResultForRoute<ApiFilmModel>(person.Films[i]).Title;
            }
            person.Films = films;
            // Get species
            string[] species = new string[person.Species.Length];
            for(var i = 0; i < person.Species.Length; i++)
            {
                species[i] = getResultForRoute<ApiSpeciesModel>(person.Species[i]).Name;
            }
            person.Species = species;
            // Get vehicles
            string[] vehicles = new string[person.Vehicles.Length];
            for(var i = 0; i < person.Vehicles.Length; i++)
            {
                vehicles[i] = getResultForRoute<ApiVehicleModel>(person.Vehicles[i]).Name;
            }
            person.Vehicles = vehicles;
            // Get starships
            var starships = new string[person.StarShips.Length];
            for(var i = 0; i < person.StarShips.Length; i++)
            {
                starships[i] = getResultForRoute<ApiStarshipModel>(person.StarShips[i]).Name;
            }
            person.StarShips = starships;
            
            return person;
        }

        public ApiPlanetModel GetPlanetById(int planetId)
        {
            var allPlanets = new List<ApiPlanetModel>();

            string url = $"https://swapi.co/api/planets/{planetId}/?format=json";
            var planet = getResultForRoute<ApiPlanetModel>(url);
            planet.Id = planetId;
            return planet;
        }

        public List<ApiPersonModel> GetResidentsOfPlanet(string planetName)
        {
            ApiPlanetModel planet = null;
            string nextUrl = "https://swapi.co/api/planets/?format=json";
            // The api returns a paginated response. 
            // To get all items, loop through until the next property is null
            while (true)
            {
                var response = getResultForRoute<ApiResponseModel<ApiPlanetModel>>(nextUrl);
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
                var resident = getResultForRoute<ApiPersonModel>(planet.Residents[i]);
                residents.Add(resident);
            }

            return residents;
        }
    }
}