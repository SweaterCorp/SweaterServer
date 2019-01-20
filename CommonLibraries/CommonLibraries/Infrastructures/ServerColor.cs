using System;
using System.Globalization;

namespace CommonLibraries.Infrastructures
{
  public class Rgb
  {
    public short R { get; set; }
    public short G { get; set; }
    public short B { get; set; }
  }

  internal class Xyz
  {
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
  }

  internal class Lab
  {
    public double L { get; set; }
    public double A { get; set; }
    public double B { get; set; }
  }

  public class ServerColor
  {
    private Rgb Rgb { get; }

    public ServerColor(Rgb rgb)
    {
      Rgb = rgb;
    }

    public ServerColor(string hex)
    {
      Rgb = ToRgb(hex);
    }

    public ServerColor(int color)
    {
      var hexValue = color.ToString("X6");
      Rgb = ToRgb(hexValue);
    }

    public string ToHex()
    {
      return ToHex(Rgb);
    }

    public int ToInt()
    {
      return int.Parse(ToHex(), NumberStyles.HexNumber);
    }

    public Rgb ToRgb()
    {
      return new Rgb {R = Rgb.R, G = Rgb.G, B = Rgb.B};
    }

    public static Rgb ToRgb(string hex)
    {
      hex = hex.Replace("#", "");
      var r = Convert.ToInt16(new string(new[] {hex[0], hex[1]}), 16);
      var g = Convert.ToInt16(new string(new[] {hex[2], hex[3]}), 16);
      var b = Convert.ToInt16(new string(new[] {hex[4], hex[5]}), 16);
      return new Rgb {R = r, G = g, B = b};
    }

    public static string ToHex(Rgb rgb)
    {
      return rgb.R.ToString("X2") + rgb.G.ToString("X2") + rgb.B.ToString("X2");
    }

    public static int ToInt(string hex)
    {
      return int.Parse(hex, NumberStyles.HexNumber);
    }

    public static double CompareRgb(ServerColor left, ServerColor right)
    {
      return CompareRgb(left.Rgb, right.Rgb);
    }

    public static double CompareRgb(Rgb left, Rgb right)
    {
      var rMean = (left.R + right.R) / 2.0;
      var deltaR = left.R - right.R;
      var deltaG = left.G - right.G;
      var deltaB = left.B - right.B;
      var deltaColor = Math.Sqrt((2 + rMean / 256) * Math.Pow(deltaR, 2) + 4 * Math.Pow(deltaG, 2) +
                                 (2 + (255 - rMean) / 256) * Math.Pow(deltaB, 2));
      return deltaColor;
    }

    public static double CompareDeltaE(ServerColor left, ServerColor right)
    {
      return CompareDeltaE(left.Rgb, right.Rgb);
    }

    public static double CompareDeltaE(Rgb left, Rgb right)
    {
      var leftColor = ToLab(ToXyz(left));
      var rightColor = ToLab(ToXyz(right));

      var num1 = 1.0;
      var num2 = 1.0;
      var num3 = 1.0;
      var lab1 = leftColor;
      var lab2 = rightColor;
      var num4 = (Math.Sqrt(lab1.A * lab1.A + lab1.B * lab1.B) + Math.Sqrt(lab2.A * lab2.A + lab2.B * lab2.B)) / 2.0;
      var num5 = num4 * num4 * num4;
      var num6 = num5 * (num5 * num4);
      var num7 = 0.5 * (1.0 - Math.Sqrt(num6 / (num6 + 6103515625.0)));
      var x1 = (1.0 + num7) * lab1.A;
      var x2 = (1.0 + num7) * lab2.A;
      var num8 = Math.Sqrt(x1 * x1 + lab1.B * lab1.B);
      var num9 = Math.Sqrt(x2 * x2 + lab2.B * lab2.B);
      var num10 = (Math.Atan2(lab1.B, x1) * 180.0 / Math.PI + 360.0) % 360.0;
      var num11 = (Math.Atan2(lab2.B, x2) * 180.0 / Math.PI + 360.0) % 360.0;
      var num12 = lab2.L - lab1.L;
      var num13 = num9 - num8;
      var num14 = Math.Abs(num10 - num11);
      var num15 = num8 * num9 != 0.0
        ? (num14 > 180.0
          ? (num14 <= 180.0 || num11 > num10 ? num11 - num10 - 360.0 : num11 - num10 + 360.0)
          : num11 - num10)
        : 0.0;
      var num16 = 2.0 * Math.Sqrt(num8 * num9) * Math.Sin(num15 * Math.PI / 360.0);
      var num17 = (lab1.L + lab2.L) / 2.0;
      var num18 = (num8 + num9) / 2.0;
      var num19 = num8 * num9 != 0.0
        ? (num14 > 180.0
          ? (num14 <= 180.0 || num10 + num11 >= 360.0 ? (num10 + num11 - 360.0) / 2.0 : (num10 + num11 + 360.0) / 2.0)
          : (num10 + num11) / 2.0)
        : 0.0;
      var num20 = num17 - 50.0;
      var num21 = num20 * num20;
      var num22 = 1.0 + 0.015 * num21 / Math.Sqrt(20.0 + num21);
      var num23 = 1.0 + 0.045 * num18;
      var num24 = 1.0 + 0.015 * (1.0 - 0.17 * Math.Cos(DegToRad(num19 - 30.0)) +
                                 0.24 * Math.Cos(DegToRad(num19 * 2.0)) + 0.32 * Math.Cos(DegToRad(num19 * 3.0 + 6.0)) -
                                 0.2 * Math.Cos(DegToRad(num19 * 4.0 - 63.0))) * num18;
      var num25 = (num19 - 275.0) / 25.0;
      var num26 = 30.0 * Math.Exp(-(num25 * num25));
      var num27 = num18 * num18 * num18;
      var num28 = num27 * (num27 * num18);
      var num29 = 2.0 * Math.Sqrt(num28 / (num28 + 6103515625.0));
      var num30 = -Math.Sin(DegToRad(2.0 * num26)) * num29;
      var num31 = num12 / (num22 * num1);
      var num32 = num13 / (num23 * num2);
      var num33 = num16 / (num24 * num3);
      return Math.Sqrt(num31 * num31 + num32 * num32 + num33 * num33 + num30 * num32 * num33);
    }

    // http://www.easyrgb.com/en/math.php 
    private static Xyz ToXyz(Rgb color)
    {
      var num1 = PivotRgb(color.R / (double) byte.MaxValue);
      var num2 = PivotRgb(color.G / (double) byte.MaxValue);
      var num3 = PivotRgb(color.B / (double) byte.MaxValue);
      var x = num1 * 0.4124 + num2 * 0.3576 + num3 * 0.1805;
      var y = num1 * 0.2126 + num2 * (447.0 / 625.0) + num3 * 0.0722;
      var z = num1 * 0.0193 + num2 * 0.1192 + num3 * 0.9505;
      return new Xyz {X = x, Y = y, Z = z};
    }

    private static Lab ToLab(Xyz color)
    {
      var whiteReference = new Xyz {X = 95.047, Y = 100.0, Z = 108.883};

      var num1 = PivotXyz(color.X / whiteReference.X);
      var num2 = PivotXyz(color.Y / whiteReference.Y);
      var num3 = PivotXyz(color.Z / whiteReference.Z);
      var l = Math.Max(0.0, 116.0 * num2 - 16.0);
      var a = 500.0 * (num1 - num2);
      var b = 200.0 * (num2 - num3);

      return new Lab {A = a, B = b, L = l};
    }

    private static double PivotXyz(double n)
    {
      if (n <= 0.008856) return (903.3 * n + 16.0) / 116.0;
      return CubicRoot(n);
    }

    private static double CubicRoot(double n)
    {
      return Math.Pow(n, 1.0 / 3.0);
    }

    private static double DegToRad(double degrees)
    {
      return degrees * Math.PI / 180.0;
    }

    private static double PivotRgb(double n)
    {
      return (n > 0.04045 ? Math.Pow((n + 0.055) / 1.055, 2.4) : n / 12.92) * 100.0;
    }
  }
}