using Newtonsoft.Json;
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
      
        private ApiResponseModel<T> getResultForRoute<T>(string url)
        {
            var result = "";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            
            using (var reader = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<ApiResponseModel<T>>(result);
        }

        public List<ApiPlanetModel> GetAllPlanets()
        {
            var listOfPlanets = new List<ApiPlanetModel>();
            string nextUrl = "https://swapi.co/api/planets/?format=json";
            // The api returns a paginated response. 
            // To get all items, loop through until the next property is null
            while (true)
            {
                var response = getResultForRoute<ApiPlanetModel>(nextUrl);
                listOfPlanets.AddRange(response.Results);
                nextUrl = response.Next;
                // Break when no next url found
                if (nextUrl == null) break;
            }

            return listOfPlanets;
        }

        public string GetAllVehicles()
        {
            throw new NotImplementedException();
        }

        public string GetPersonDetails(int personId)
        {
            throw new NotImplementedException();
        }

        public string GetPlanetById(int planetId)
        {
            throw new NotImplementedException();
        }

        public string GetResidentsOfPlanet(int planetId)
        {
            throw new NotImplementedException();
        }
    }
}