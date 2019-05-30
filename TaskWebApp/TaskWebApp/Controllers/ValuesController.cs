using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace TaskWebApp.Controllers
{
     public class ValuesController : ApiController
     {
          // GET api/values
          public IEnumerable<string> Get()
          {
               return new string[] { "value1", "value2" };
          }

          // GET api/values/5
          public string Get(int id)
          {
               Thread.Sleep(5000);

               return "value";
          }
     }
}
