using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibraries.Infrastructures;
using CommonLibraries.Localization;

namespace CommonLibraries.CommonTypes
{
  public class SexType : Enumeration
  {
    private List<TranslationName> _translationNames = new List<TranslationName>();

    public static SexType None { get; } = new SexType(0, "None")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "None"),
        new TranslationName(LaguageType.English, "None"),
        new TranslationName(LaguageType.Russian, "Неизвестно")
      }
    };

    public static SexType Male { get; } = new SexType(1, "Male")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Male"),
        new TranslationName(LaguageType.English, "Male"),
        new TranslationName(LaguageType.Russian, "Мужской")
      }
    };

    public static SexType Female { get; } = new SexType(2, "Female")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Female"),
        new TranslationName(LaguageType.English, "Female"),
        new TranslationName(LaguageType.Russian, "Женщина")
      }
    };

    public string this[LaguageType laguageType] => _translationNames.FirstOrDefault(x => x.LaguageType == laguageType)
                                                     ?.Name ?? _translationNames[0].Name;

    private SexType(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<SexType> List()
    {
      return new List<SexType> {None, Male, Female};
    }

    public static SexType FromKey(string key)
    {
      return FromString(key, List());
    }

    public static SexType FromValue(int id)
    {
      return FromValue(id, List());
    }

    public static bool IsValid(int id)
    {
      return IsValid(id, List());
    }

    public static implicit operator SexType(int x)
    {
      return List().FirstOrDefault(item => item.Id == x) ??
             throw new InvalidCastException($"Cannot cast int x:{x} to enumeration {nameof(SexType)}");
    }

    public static explicit operator int(SexType sexType)
    {
      return sexType.Id;
    }
  }
}