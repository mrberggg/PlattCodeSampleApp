using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Exceptions
{
    public class PlanetNotFoundException : Exception
    {
        public PlanetNotFoundException()
        {

        }
        public PlanetNotFoundException(string message): base(message)
        {

        }
    }
}