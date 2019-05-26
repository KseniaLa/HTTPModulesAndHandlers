using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace RequestPipeline.HttpHandlers
{
     public class CropImageHandler : IHttpHandler
     {
          public bool IsReusable
          {
               get { return false; }
          }

          public void ProcessRequest(HttpContext context)
          {
               var imagePath = $"{AppDomain.CurrentDomain.BaseDirectory}{context.Request.FilePath}";

               if (!File.Exists(imagePath))
               {
                    context.Response.StatusCode = 400;
                    context.Response.Write("File not found");
                    context.Response.End();
                    return;
               }

               var img = new Bitmap(imagePath);

               var requestUrl = new Uri(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.RawUrl);

               var sizeParameter = HttpUtility.ParseQueryString(requestUrl.Query).Get("size");

               var temp = new MemoryStream();

               if (!string.IsNullOrEmpty(sizeParameter))
               {
                    var size = int.Parse(sizeParameter);
                    img = CropImage(img, new Rectangle(0, 0, size, size));
               }

               img.Save(temp, ImageFormat.Jpeg);
               var buffer = temp.GetBuffer();
               context.Response.OutputStream.Write(buffer, 0, buffer.Length);
               context.Response.End();
          }

          public Bitmap CropImage(Bitmap source, Rectangle section)
          {
               var bmp = new Bitmap(section.Width, section.Height);

               var g = Graphics.FromImage(bmp);

               g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

               return bmp;
          }
     }
}
