﻿using Newtonsoft.Json;
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
            throw new NotImplementedException();
        }

        public ApiPersonModel GetPersonDetails(int personId)
        {
            throw new NotImplementedException();
        }

        public ApiPlanetModel GetPlanetById(int planetId)
        {
            string nextUrl = "https://swapi.co/api/planets/" + planetId + "/?format=json";
            var planet = getResultForRoute<ApiPlanetModel>(nextUrl);

            return planet;
        }

        public List<ApiPersonModel> GetResidentsOfPlanet(int planetId)
        {
            throw new NotImplementedException();
        }
    }
}