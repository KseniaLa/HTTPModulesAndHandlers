using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestPipeline.Extensions
{
     internal static class StringImageFormatExtension
     {
          public static string GetContentTypeByName(this string path)
          {
               var extension = path.ToLower().Substring(path.LastIndexOf('.') + 1);

               switch (extension)
               {
                    case "jpeg":
                    case "jpg":
                         return "image/jpeg";
                    case "png":
                         return "image/png";
                    case "bmp":
                         return "image/bmp";
                    case "tif":
                    case "tiff":
                         return "image/tiff";
                    default:
                         return "image/xyz";
               }
          }

          public static ImageFormat GetImageFormatByName(this string path)
          {
               var extension = path.ToLower().Substring(path.LastIndexOf('.') + 1);

               switch (extension)
               {
                    case "jpeg":
                    case "jpg":
                         return ImageFormat.Jpeg;
                    case "png":
                         return ImageFormat.Png;
                    case "bmp":
                         return ImageFormat.Bmp;
                    case "tif":
                    case "tiff":
                         return ImageFormat.Tiff;
                    default:
                         return ImageFormat.Jpeg;
               }
          }
     }
}
