using System;
using System.Diagnostics;
using System.Threading;
using System.Web;

namespace RequestPipeline.HttpModules
{
     public class RequestHeadersModule : IHttpModule
     {
          public void Dispose()
          {

          }

          public void Init(HttpApplication context)
          {
               context.BeginRequest += OnBegin;
               context.EndRequest += OnEnd;
          }

          public void OnBegin(object source, EventArgs e)
          {
               var context = ((HttpApplication)source).Context;

               context.Items["Timer"] = new TimeSpan(DateTime.UtcNow.Ticks).TotalMilliseconds;

               context.Response.AddHeader("IP address", context.Request.UserHostAddress ?? "undefined");
          }

          public void OnEnd(object source, EventArgs e)
          {
               var context = ((HttpApplication)source).Context;

               if (context.Items["Timer"] is double startTime)
               {
                    var endTime = new TimeSpan(DateTime.UtcNow.Ticks).TotalMilliseconds;

                    context.Response.AddHeader("Total Time", $"{Math.Abs(endTime - startTime)} ms");
               }
          }

     }
}
