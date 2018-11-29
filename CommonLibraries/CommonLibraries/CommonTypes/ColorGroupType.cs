using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibraries.Infrastructures;
using CommonLibraries.Localization;

namespace CommonLibraries.CommonTypes
{
  public class ColorGroupType : Enumeration
  {
    private List<TranslationName> _translationNames = new List<TranslationName>();

    public static ColorGroupType None { get; } = new ColorGroupType(0, "None", new List<ColorType>())
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "None"),
        new TranslationName(LaguageType.English, "None"),
        new TranslationName(LaguageType.Russian, "Неизвестно")
      }
    };

    // ---------- 

    public static ColorGroupType RedPink { get; } =
      new ColorGroupType(1, "RedPink", new List<ColorType> {ColorType.Red, ColorType.Pink})
      {
        _translationNames = new List<TranslationName>
        {
          new TranslationName(LaguageType.Default, "RedPink"),
          new TranslationName(LaguageType.English, "Red, pink"),
          new TranslationName(LaguageType.Russian, "Красный, розовый")
        }
      };

    public static ColorGroupType OrangeYellow { get; } =
      new ColorGroupType(2, "OrangeYellow", new List<ColorType> {ColorType.Orange, ColorType.Yellow})
      {
        _translationNames = new List<TranslationName>
        {
          new TranslationName(LaguageType.Default, "Orange and yellow"),
          new TranslationName(LaguageType.English, "Orange and yellow"),
          new TranslationName(LaguageType.Russian, "Оранжевый и желтый")
        }
      };

    public static ColorGroupType Green { get; } =
      new ColorGroupType(3, "Green", new List<ColorType> {ColorType.Green})
      {
        _translationNames = new List<TranslationName>
        {
          new TranslationName(LaguageType.Default, "Green"),
          new TranslationName(LaguageType.English, "Green"),
          new TranslationName(LaguageType.Russian, "Зеленый")
        }
      };

    public static ColorGroupType Blue { get; } = new ColorGroupType(4, "Blue", new List<ColorType> {ColorType.Blue})
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Blue"),
        new TranslationName(LaguageType.English, "Blue"),
        new TranslationName(LaguageType.Russian, "Голубой")
      }
    };

    public static ColorGroupType Purple { get; } =
      new ColorGroupType(5, "Purple", new List<ColorType> {ColorType.Purple})
      {
        _translationNames = new List<TranslationName>
        {
          new TranslationName(LaguageType.Default, "Purple"),
          new TranslationName(LaguageType.English, "Purple"),
          new TranslationName(LaguageType.Russian, "Пурпурный")
        }
      };

    public static ColorGroupType BrownBeige { get; } =
      new ColorGroupType(6, "BrownBeige", new List<ColorType> {ColorType.Brown, ColorType.Beige})
      {
        _translationNames = new List<TranslationName>
        {
          new TranslationName(LaguageType.Default, "Brown and beige"),
          new TranslationName(LaguageType.English, "Brown and beige"),
          new TranslationName(LaguageType.Russian, "Коричневый и бежевый")
        }
      };

    public static ColorGroupType GrayBlackWhite { get; } =
      new ColorGroupType(7, "GrayBlackWhite", new List<ColorType> {ColorType.Gray, ColorType.Black, ColorType.White})
      {
        _translationNames = new List<TranslationName>
        {
          new TranslationName(LaguageType.Default, "Gray, black and white"),
          new TranslationName(LaguageType.English, "Gray, black and white"),
          new TranslationName(LaguageType.Russian, "Серый, черный и белый")
        }
      };

    public string this[LaguageType laguageType] => _translationNames.FirstOrDefault(x => x.LaguageType == laguageType)
                                                     ?.Name ?? _translationNames[0].Name;

    public List<ColorType> Colors { get; }

    private ColorGroupType(int id, string name, List<ColorType> colors) : base(id, name)
    {
      Colors = colors;
    }

    public static IEnumerable<ColorGroupType> List()
    {
      return new List<ColorGroupType> {None, RedPink, OrangeYellow, Green, Blue, Purple, BrownBeige, GrayBlackWhite};
    }

    public static ColorGroupType FromKey(string key)
    {
      return FromString(key, List());
    }

    public static ColorGroupType FromValue(int id)
    {
      return FromValue(id, List());
    }

    public static bool IsValid(int id)
    {
      return IsValid(id, List());
    }

    public static implicit operator ColorGroupType(int x)
    {
      return List().FirstOrDefault(item => item.Id == x) ??
             throw new InvalidCastException($"Cannot cast int x:{x} to enumeration {nameof(ColorGroupType)}");
    }

    public static explicit operator int(ColorGroupType sexType)
    {
      return sexType.Id;
    }
  }
}