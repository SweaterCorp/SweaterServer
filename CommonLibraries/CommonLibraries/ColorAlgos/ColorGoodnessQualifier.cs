using System.Collections.Generic;
using System.Linq;
using CommonLibraries.CommonTypes;
using CommonLibraries.Infrastructures;
using CommonLibraries.Infrastructures.ColorsData;
using CommonLibraries.Resources;
using CommonLibraries.Resources.DeserializerTypes;

namespace CommonLibraries.ColorAlgos
{
  public class ColorGoodnessQualifier
  {
    private ContentPath ContentPath { get; } = new ContentPath();
    private ResourceHandler ResourceHandler { get; } = new ResourceHandler();

    private ColorMatching ColorMatching { get; set; }

    public void Init()
    {
      var deserizlizer = ResourceHandler.ReadeResourceFile<ColorMatchingDeserializer>(ContentPath.ColorsMatching);
      ColorMatching = ColorMatching.FromColorsMatchingDeserializer(deserizlizer);
    }

    public double GetColorGoodness(PersonalColorType personalColorType, ServerColor color)
    {
      var colorGroups = ColorMatching.Autumn;
      if (personalColorType == PersonalColorType.Winter) colorGroups = ColorMatching.Winter;
      if (personalColorType == PersonalColorType.Spring) colorGroups = ColorMatching.Spring;
      if (personalColorType == PersonalColorType.Summer) colorGroups = ColorMatching.Summer;
      if (personalColorType == PersonalColorType.Autumn) colorGroups = ColorMatching.Autumn;

      var red = GetColorGoodness(colorGroups.RedPink.GoodColors, colorGroups.RedPink.BadColors, color);
      var orangeYellow = GetColorGoodness(colorGroups.OrangeYellow.GoodColors, colorGroups.OrangeYellow.BadColors, color);
      var green = GetColorGoodness(colorGroups.Green.GoodColors, colorGroups.Green.BadColors, color);
      var blue = GetColorGoodness(colorGroups.Blue.GoodColors, colorGroups.Blue.BadColors, color);
      var brownBeige = GetColorGoodness(colorGroups.BrownBeige.GoodColors, colorGroups.BrownBeige.BadColors, color);
      var grayBlackWhite = GetColorGoodness(colorGroups.GrayBlackWhite.GoodColors, colorGroups.GrayBlackWhite.BadColors, color);

      return new List<double> {red, orangeYellow, green, blue, brownBeige, grayBlackWhite}.Max();
    }

    private static double GetColorGoodness(IEnumerable<ServerColor> goodColors, IEnumerable<ServerColor> badColors,
      ServerColor color)
    {
      var minGoodDiff = goodColors.Select(goodColor => ServerColor.CompareDeltaE(color, goodColor)).Min();
      var minBadDiff = badColors.Select(badColor => ServerColor.CompareDeltaE(color, badColor)).Min();

      return 1 - minGoodDiff / (minGoodDiff + minBadDiff);
    }
  }
}