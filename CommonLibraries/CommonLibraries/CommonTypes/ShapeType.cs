using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibraries.Infrastructures;
using CommonLibraries.Localization;

namespace CommonLibraries.CommonTypes
{
  public class ShapeType : Enumeration
  {
    private List<TranslationName> _translationNames = new List<TranslationName>();

    public static ShapeType None { get; } = new ShapeType(0, "None")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "None"),
        new TranslationName(LaguageType.English, "None"),
        new TranslationName(LaguageType.Russian, "Неизвестно")
      }
    };

    public static ShapeType Hourglass { get; } = new ShapeType(1, "Male")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Hourglass"),
        new TranslationName(LaguageType.English, "Hourglass"),
        new TranslationName(LaguageType.Russian, "Песочные часы")
      }
    };

    public static ShapeType Rectangle { get; } = new ShapeType(2, "Rectangle")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Rectangle"),
        new TranslationName(LaguageType.English, "Rectangle"),
        new TranslationName(LaguageType.Russian, "Прямоугольник")
      }
    };

    public static ShapeType Pear { get; } = new ShapeType(3, "Pear")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Pear"),
        new TranslationName(LaguageType.English, "Pear"),
        new TranslationName(LaguageType.Russian, "Груша")
      }
    };

    public static ShapeType InvertedTriangle { get; } = new ShapeType(4, "InvertedTriangle")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Inverted triangle"),
        new TranslationName(LaguageType.English, "Pear"),
        new TranslationName(LaguageType.Russian, "Груша")
      }
    };

    public static ShapeType Apple { get; } = new ShapeType(5, "Apple")
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

    private ShapeType(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<ShapeType> List()
    {
      return new List<ShapeType> {None, Hourglass, Rectangle, Pear, InvertedTriangle, Apple};
    }

    public static ShapeType FromKey(string key)
    {
      return FromString(key, List());
    }

    public static ShapeType FromValue(int id)
    {
      return FromValue(id, List());
    }

    public static bool IsValid(int id)
    {
      return IsValid(id, List());
    }

    public static implicit operator ShapeType(int x)
    {
      return List().FirstOrDefault(item => item.Id == x) ??
             throw new InvalidCastException($"Cannot cast int x:{x} to enumeration {nameof(ShapeType)}");
    }

    public static explicit operator int(ShapeType shapeType)
    {
      return shapeType.Id;
    }
  }
}