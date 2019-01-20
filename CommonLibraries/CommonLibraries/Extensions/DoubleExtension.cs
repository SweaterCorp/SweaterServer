using System;

namespace CommonLibraries.Extensions
{
  public static class DoubleExtension
  {
    public static bool EqualsStrict(this double left, double right, double tolerance = 0.000001)
    {
      return Math.Abs(left - right) < tolerance;
    }
  }
}