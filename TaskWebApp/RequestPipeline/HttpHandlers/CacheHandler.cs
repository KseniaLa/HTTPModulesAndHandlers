using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Web;

namespace RequestPipeline.HttpHandlers
{
     public class CacheHandler : IHttpHandler
     {
          public bool IsReusable => false;

          public void ProcessRequest(HttpContext context)
          {
               var imagePath = $"{AppDomain.CurrentDomain.BaseDirectory}{context.Request.FilePath}";

               if (!File.Exists(imagePath))
               {
                    context.Response.StatusCode = 404;
                    context.Response.StatusDescription = "File not found";

                    context.Response.End();
                    return;
               }

               var imgInfo = new FileInfo(imagePath); 

               if (!string.IsNullOrEmpty(context.Request.Headers["If-Modified-Since"]))
               {
                    var lastMod = DateTime.ParseExact(context.Request.Headers["If-Modified-Since"], "r",
                         CultureInfo.InvariantCulture).ToLocalTime();

                    if (lastMod == imgInfo.LastWriteTime.TrimMilliseconds())
                    {
                         context.Response.StatusCode = 304;
                         context.Response.StatusDescription = "Not Modified";

                         context.Response.End();
                         return;
                    }
               }

               var buffer = File.ReadAllBytes(imagePath);

               context.Response.OutputStream.Write(buffer, 0, buffer.Length);
               context.Response.Cache.SetCacheability(HttpCacheability.Public);
               context.Response.Cache.SetLastModified(imgInfo.LastWriteTime);
               context.Response.ContentType = "image/jpeg";

               context.Response.End();
          }
     }

     public static class DateTimeExtensions
     {
          public static DateTime TrimMilliseconds(this DateTime dt)
          {
               return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0, dt.Kind);
          }
     }
}
