using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace PlattSampleApp.Services
{
    public class HttpService : IHttpService
    {
        public T Get<T>(string url)
        {
            var result = "";
            var request = WebRequest.Create(url) as HttpWebRequest;

            using (var reader = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}