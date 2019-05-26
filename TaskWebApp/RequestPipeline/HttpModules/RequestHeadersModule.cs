using System;
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

          public void OnBegin(Object source, EventArgs e)
          {
               var context = ((HttpApplication)source).Context;

               context.Response.AddHeader("Start Time", DateTime.Now.ToString("O"));
               context.Response.AddHeader("IP address", context.Request.UserHostAddress ?? "undefined");
          }

          public void OnEnd(Object source, EventArgs e)
          {
               var context = ((HttpApplication)source).Context;

               context.Response.AddHeader("End Time", DateTime.Now.ToString("O"));
          }

     }
}
