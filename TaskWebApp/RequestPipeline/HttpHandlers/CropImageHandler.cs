using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using RequestPipeline.Extensions;

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
                    context.Response.StatusCode = 404;
                    context.Response.StatusDescription = "File not found";
                    context.Response.End();
                    return;
               }

               var img = new Bitmap(imagePath);

               var sizeParameter = HttpUtility.ParseQueryString(context.Request.Url.Query).Get("size");

               var temp = new MemoryStream();

               if (!string.IsNullOrEmpty(sizeParameter))
               {
                    if (int.TryParse(sizeParameter, out var size) && size > 0)
                    {
                         img = CropImage(img, size, size);
                    }
               }

               img.Save(temp, context.Request.FilePath.GetImageFormatByName());
               var buffer = temp.GetBuffer();
               context.Response.OutputStream.Write(buffer, 0, buffer.Length);
               context.Response.ContentType = context.Request.FilePath.GetContentTypeByName();

               context.Response.End();
          }

          public Bitmap CropImage(Bitmap source, int width, int height)
          {
               var cropWidth = source.Width >= width ? width : source.Width;
               var cropHeight = source.Height >= height ? height : source.Height;

               var bmp = new Bitmap(cropWidth, cropHeight);

               var g = Graphics.FromImage(bmp);

               g.DrawImage(source, 0, 0, new Rectangle(0, 0, cropWidth, cropHeight), GraphicsUnit.Pixel);

               return bmp;
          }

          
     }
}
