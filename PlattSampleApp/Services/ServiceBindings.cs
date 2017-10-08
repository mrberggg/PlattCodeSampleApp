using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Services
{
    public class ServiceBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IHttpService>().To<HttpService>();
            Bind<IStarWarsService>().To<SWAPIStarWarsService>();
        }
    }
}