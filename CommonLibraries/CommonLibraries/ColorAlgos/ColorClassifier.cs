using System.Collections.Generic;
using System.Linq;
using CommonLibraries.CommonTypes;
using CommonLibraries.Infrastructures;
using CommonLibraries.Infrastructures.ColorsData;
using CommonLibraries.Resources;
using CommonLibraries.Resources.DeserializerTypes;

namespace CommonLibraries.ColorAlgos
{
  public class ColorClassifier
  {
    public ContentPath ContentPath { get; set; } = new ContentPath();
    public ResourceHandler ResourceHandler { get; set; } = new ResourceHandler();

    public ColorMatching ColorMatching { get; private set; }

    public void Init()
    {
      var deserizlizer = ResourceHandler.ReadeResourceFile<ColorMatchingDeserializer>(ContentPath.ColorsMatching);
      ColorMatching = ColorMatching.FromColorsMatchingDeserializer(deserizlizer);
    }

    public double GetColorGoodness(PersonalColorType personalColorType, ColorGroupType colorGroupType,
      ServerColor color)
    {
      var colorGroups = ColorMatching.Autumn;
      if (personalColorType == PersonalColorType.Winter) colorGroups = ColorMatching.Winter;
      if (personalColorType == PersonalColorType.Spring) colorGroups = ColorMatching.Spring;
      if (personalColorType == PersonalColorType.Summer) colorGroups = ColorMatching.Summer;
      if (personalColorType == PersonalColorType.Autumn) colorGroups = ColorMatching.Autumn;

      var colorGroup = colorGroups.RedPink;
      if (colorGroupType == ColorGroupType.RedPink) colorGroup = colorGroups.RedPink;
      if (colorGroupType == ColorGroupType.OrangeYellow) colorGroup = colorGroups.OrangeYellow;
      if (colorGroupType == ColorGroupType.Green) colorGroup = colorGroups.Green;
      if (colorGroupType == ColorGroupType.Blue) colorGroup = colorGroups.Blue;
      if (colorGroupType == ColorGroupType.Purple) colorGroup = colorGroups.Purple;
      if (colorGroupType == ColorGroupType.BrownBeige) colorGroup = colorGroups.BrownBeige;
      if (colorGroupType == ColorGroupType.GrayBlackWhite) colorGroup = colorGroups.GrayBlackWhite;

      return GetColorGoodness(colorGroup.GoodColors, colorGroup.BadColors, color);
    }

    public ColorGoodness GetColorGoodness(PersonalColorType personalColorType, ServerColor color)
    {
      var colorGroups = ColorMatching.Autumn;
      if (personalColorType == PersonalColorType.Winter) colorGroups = ColorMatching.Winter;
      if (personalColorType == PersonalColorType.Spring) colorGroups = ColorMatching.Spring;
      if (personalColorType == PersonalColorType.Summer) colorGroups = ColorMatching.Summer;
      if (personalColorType == PersonalColorType.Autumn) colorGroups = ColorMatching.Autumn;

      var result = new ColorGoodness
      {
        RedPink =
          new ColorSimilarity
          {
            ColorGroupType = ColorGroupType.RedPink,
            Similarity = GetColorGoodness(colorGroups.RedPink.GoodColors, colorGroups.RedPink.BadColors, color)
          },
        OrangeYellow =
          new ColorSimilarity
          {
            ColorGroupType = ColorGroupType.OrangeYellow,
            Similarity = GetColorGoodness(colorGroups.OrangeYellow.GoodColors, colorGroups.OrangeYellow.BadColors,
              color)
          },
        Green =
          new ColorSimilarity
          {
            ColorGroupType = ColorGroupType.Green,
            Similarity = GetColorGoodness(colorGroups.Green.GoodColors, colorGroups.Green.BadColors, color)
          },
        Blue =
          new ColorSimilarity
          {
            ColorGroupType = ColorGroupType.Blue,
            Similarity = GetColorGoodness(colorGroups.Blue.GoodColors, colorGroups.Blue.BadColors, color)
          },
        Purple =
          new ColorSimilarity
          {
            ColorGroupType = ColorGroupType.Purple,
            Similarity = GetColorGoodness(colorGroups.Purple.GoodColors, colorGroups.Purple.BadColors, color)
          },
        BrownBeige =
          new ColorSimilarity
          {
            ColorGroupType = ColorGroupType.BrownBeige,
            Similarity = GetColorGoodness(colorGroups.BrownBeige.GoodColors, colorGroups.BrownBeige.BadColors, color)
          },
        GrayBlackWhite = new ColorSimilarity
        {
          ColorGroupType = ColorGroupType.GrayBlackWhite,
          Similarity = GetColorGoodness(colorGroups.GrayBlackWhite.GoodColors, colorGroups.GrayBlackWhite.BadColors,
            color)
        }
      };
      return result;
    }

    private static double GetColorGoodness(IEnumerable<ServerColor> goodColors, IEnumerable<ServerColor> badColors,
      ServerColor color)
    {
      var minGoodDiff = goodColors.Select(goodColor => ServerColor.CompareDeltaE(color, goodColor)).Min();
      var minBadDiff = badColors.Select(badColor => ServerColor.CompareDeltaE(color, badColor)).Min();

      return 1 - minGoodDiff / (minGoodDiff + minBadDiff);
    }
  }

  public class ColorGoodness
  {
    public ColorSimilarity RedPink { get; set; }
    public ColorSimilarity OrangeYellow { get; set; }
    public ColorSimilarity Green { get; set; }
    public ColorSimilarity Blue { get; set; }
    public ColorSimilarity Purple { get; set; }
    public ColorSimilarity BrownBeige { get; set; }
    public ColorSimilarity GrayBlackWhite { get; set; }
  }

  public class ColorSimilarity
  {
    public ColorGroupType ColorGroupType { get; set; }
    public double Similarity { get; set; }
  }
}