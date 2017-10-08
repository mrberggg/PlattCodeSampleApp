using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Exceptions
{
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException()
        {

        }
        public PersonNotFoundException(string message) : base(message)
        {

        }
    }
}