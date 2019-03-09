using System;

namespace SweaterMain.Infrastructure
{
  public class UrlConverter
  {
    public static string Convert(string url, ConvertedType type)
    {
      var uri = new Uri(url);
      var size = uri.LocalPath.Split("/")[1];

      return url.Replace(size, type.ToString().ToLower());
    }
  }

  public enum ConvertedType
  {
    Product = 0,
    Img46X66 = 1,
    Img135X194 = 2
  }
}