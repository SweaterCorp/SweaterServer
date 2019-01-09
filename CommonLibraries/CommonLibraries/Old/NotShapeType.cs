using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibraries.Infrastructures;
using CommonLibraries.Localization;

namespace CommonLibraries.Old
{
  public class NotShapeType : NotEnumeration
  {
    private List<TranslationName> _translationNames = new List<TranslationName>();

    public static NotShapeType None { get; } = new NotShapeType(0, "None")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "None"),
        new TranslationName(LaguageType.English, "None"),
        new TranslationName(LaguageType.Russian, "Неизвестно")
      }
    };

    public static NotShapeType Hourglass { get; } = new NotShapeType(1, "Male")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Hourglass"),
        new TranslationName(LaguageType.English, "Hourglass"),
        new TranslationName(LaguageType.Russian, "Песочные часы")
      }
    };

    public static NotShapeType Rectangle { get; } = new NotShapeType(2, "Rectangle")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Rectangle"),
        new TranslationName(LaguageType.English, "Rectangle"),
        new TranslationName(LaguageType.Russian, "Прямоугольник")
      }
    };

    public static NotShapeType Pear { get; } = new NotShapeType(3, "Pear")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Pear"),
        new TranslationName(LaguageType.English, "Pear"),
        new TranslationName(LaguageType.Russian, "Груша")
      }
    };

    public static NotShapeType InvertedTriangle { get; } = new NotShapeType(4, "InvertedTriangle")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Inverted triangle"),
        new TranslationName(LaguageType.English, "Pear"),
        new TranslationName(LaguageType.Russian, "Груша")
      }
    };

    public static NotShapeType Apple { get; } = new NotShapeType(5, "Apple")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Apple"),
        new TranslationName(LaguageType.English, "Apple"),
        new TranslationName(LaguageType.Russian, "Яблоко")
      }
    };

    public string this[LaguageType laguageType] => _translationNames.FirstOrDefault(x => x.LaguageType == laguageType)
                                                     ?.Name ?? _translationNames[0].Name;

    private NotShapeType(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<NotShapeType> List()
    {
      return new List<NotShapeType> {None, Hourglass, Rectangle, Pear, InvertedTriangle, Apple};
    }

    public static NotShapeType FromKey(string key)
    {
      return FromString(key, List());
    }

    public static NotShapeType FromValue(int id)
    {
      return FromValue(id, List());
    }

    public static bool IsValid(int id)
    {
      return IsValid(id, List());
    }

    public static implicit operator NotShapeType(int x)
    {
      return List().FirstOrDefault(item => item.Id == x) ??
             throw new InvalidCastException($"Cannot cast int x:{x} to enumeration {nameof(NotShapeType)}");
    }

    public static explicit operator int(NotShapeType shapeType)
    {
      return shapeType.Id;
    }
  }
}