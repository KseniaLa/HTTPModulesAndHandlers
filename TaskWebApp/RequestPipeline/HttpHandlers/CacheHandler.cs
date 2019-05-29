using System;
using System.Web;

namespace RequestPipeline.HttpHandlers
{
     public class CacheHandler : IHttpHandler
     {
          public bool IsReusable
          {
               get { return true; }
          }

          public void ProcessRequest(HttpContext context)
          {
               
          }
     }
}
