using Newtonsoft.Json;
using PlattSampleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlattSampleAppTests.Services
{
    class HttpServiceMock : IHttpService
    {
        // Use a queue so that we can get multiple responses. FIFO
        public Dictionary<string, string> JsonResponses = new Dictionary<string, string>();

        public T Get<T>(string url)
        {
            return JsonConvert.DeserializeObject<T>(JsonResponses[url]);  
        }
    }
}
