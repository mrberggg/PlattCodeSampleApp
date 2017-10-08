using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Services
{
    public interface IHttpService
    {
        T Get<T>(string url);
    }
}