using System;

namespace CommonLibraries.Infrastructures
{
  public class RGB
  {
    public short R { get; set; }
    public short G { get; set; }
    public short B { get; set; }
  }

  public class ServerColor
  {
    private RGB RGB { get; }

    public ServerColor(RGB rgb)
    {
      RGB = rgb;
    }

    public ServerColor(string hex)
    {
      RGB = ToRgb(hex);
    }

    public string ToHex()
    {
      return ToHex(RGB);
    }

    public RGB ToRgb()
    {
      return RGB;
    }

    public static RGB ToRgb(string hex)
    {
      hex = hex.Replace("#", "");
      var r = Convert.ToInt16(new string(new[] {hex[0], hex[1]}));
      var g = Convert.ToInt16(new string(new[] {hex[2], hex[3]}));
      var b = Convert.ToInt16(new string(new[] {hex[4], hex[5]}));
      return new RGB {R = r, G = g, B = b};
    }

    public static string ToHex(RGB rgb)
    {
      return rgb.R.ToString("X2") + rgb.G.ToString("X2") + rgb.B.ToString("X2");
    }

    public static double GetDifference(ServerColor left, ServerColor right)
    {
      var rMean = (left.RGB.R + right.RGB.R) / 2.0;
      var deltaR = left.RGB.R - right.RGB.R;
      var deltaG = left.RGB.G - right.RGB.G;
      var deltaB = left.RGB.B - right.RGB.B;
      var deltaColor = Math.Sqrt((2 + rMean / 256) * Math.Pow(deltaR, 2) + 4 * Math.Pow(deltaG, 2) +
                                 (2 + (255 - rMean) / 256) + Math.Pow(deltaB, 2));
      return deltaColor;
    }
  }
}