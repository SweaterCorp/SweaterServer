using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibraries.Infrastructures;
using CommonLibraries.Localization;

namespace CommonLibraries.Old
{
  public class NotColorGroupType : NotEnumeration
  {
    private List<TranslationName> _translationNames = new List<TranslationName>();

    public static NotColorGroupType None { get; } = new NotColorGroupType(0, "None", new List<NotColorType>())
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "None"),
        new TranslationName(LaguageType.English, "None"),
        new TranslationName(LaguageType.Russian, "Неизвестно")
      }
    };

    // ---------- 

    public static NotColorGroupType RedPink { get; } =
      new NotColorGroupType(1, "RedPink", new List<NotColorType> {NotColorType.Red, NotColorType.Pink})
      {
        _translationNames = new List<TranslationName>
        {
          new TranslationName(LaguageType.Default, "RedPink"),
          new TranslationName(LaguageType.English, "Red, pink"),
          new TranslationName(LaguageType.Russian, "Красный, розовый")
        }
      };

    public static NotColorGroupType OrangeYellow { get; } =
      new NotColorGroupType(2, "OrangeYellow", new List<NotColorType> {NotColorType.Orange, NotColorType.Yellow})
      {
        _translationNames = new List<TranslationName>
        {
          new TranslationName(LaguageType.Default, "Orange and yellow"),
          new TranslationName(LaguageType.English, "Orange and yellow"),
          new TranslationName(LaguageType.Russian, "Оранжевый и желтый")
        }
      };

    public static NotColorGroupType Green { get; } =
      new NotColorGroupType(3, "Green", new List<NotColorType> {NotColorType.Green})
      {
        _translationNames = new List<TranslationName>
        {
          new TranslationName(LaguageType.Default, "Green"),
          new TranslationName(LaguageType.English, "Green"),
          new TranslationName(LaguageType.Russian, "Зеленый")
        }
      };

    public static NotColorGroupType Blue { get; } = new NotColorGroupType(4, "Blue", new List<NotColorType> {NotColorType.Blue})
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Blue"),
        new TranslationName(LaguageType.English, "Blue"),
        new TranslationName(LaguageType.Russian, "Голубой")
      }
    };

    public static NotColorGroupType Purple { get; } =
      new NotColorGroupType(5, "Purple", new List<NotColorType> {NotColorType.Purple})
      {
        _translationNames = new List<TranslationName>
        {
          new TranslationName(LaguageType.Default, "Purple"),
          new TranslationName(LaguageType.English, "Purple"),
          new TranslationName(LaguageType.Russian, "Пурпурный")
        }
      };

    public static NotColorGroupType BrownBeige { get; } =
      new NotColorGroupType(6, "BrownBeige", new List<NotColorType> {NotColorType.Brown, NotColorType.Beige})
      {
        _translationNames = new List<TranslationName>
        {
          new TranslationName(LaguageType.Default, "Brown and beige"),
          new TranslationName(LaguageType.English, "Brown and beige"),
          new TranslationName(LaguageType.Russian, "Коричневый и бежевый")
        }
      };

    public static NotColorGroupType GrayBlackWhite { get; } =
      new NotColorGroupType(7, "GrayBlackWhite", new List<NotColorType> {NotColorType.Gray, NotColorType.Black, NotColorType.White})
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

    public List<NotColorType> Colors { get; }

    private NotColorGroupType(int id, string name, List<NotColorType> colors) : base(id, name)
    {
      Colors = colors;
    }

    public static IEnumerable<NotColorGroupType> List()
    {
      return new List<NotColorGroupType> {None, RedPink, OrangeYellow, Green, Blue, Purple, BrownBeige, GrayBlackWhite};
    }

    public static NotColorGroupType FromKey(string key)
    {
      return FromString(key, List());
    }

    public static NotColorGroupType FromValue(int id)
    {
      return FromValue(id, List());
    }

    public static bool IsValid(int id)
    {
      return IsValid(id, List());
    }

    public static implicit operator NotColorGroupType(int x)
    {
      return List().FirstOrDefault(item => item.Id == x) ??
             throw new InvalidCastException($"Cannot cast int x:{x} to enumeration {nameof(NotColorGroupType)}");
    }

    public static explicit operator int(NotColorGroupType sexType)
    {
      return sexType.Id;
    }
  }
}