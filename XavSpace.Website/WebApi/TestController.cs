using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace XavSpace.Website.WebApi
{
    public class TestController : ApiController
    {
        // GET: /api/Test
        public IEnumerable<string> Get()
        {
            return new string[] { "Galdin Raphael", "Joshua Dias" };
        }
    }
}