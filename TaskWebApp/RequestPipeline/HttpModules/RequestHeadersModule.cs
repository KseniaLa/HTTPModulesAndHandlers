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

               var timer =new Stopwatch();
               context.Items["Timer"] = timer;

               context.Response.AddHeader("IP address", context.Request.UserHostAddress ?? "undefined");

               timer.Start();
          }

          public void OnEnd(object source, EventArgs e)
          {
               var context = ((HttpApplication)source).Context;

               if (context.Items["Timer"] is Stopwatch timer)
               {
                    timer.Stop();

                    context.Response.AddHeader("Total Time", $"{timer.Elapsed.TotalMilliseconds} ms");
               }
          }

     }
}
