using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibraries.Infrastructures;
using CommonLibraries.Localization;

namespace CommonLibraries.Old
{
  public class NotSexType : Enumeration
  {
    private List<TranslationName> _translationNames = new List<TranslationName>();

    public static NotSexType None { get; } = new NotSexType(0, "None")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "None"),
        new TranslationName(LaguageType.English, "None"),
        new TranslationName(LaguageType.Russian, "Неизвестно")
      }
    };

    public static NotSexType Male { get; } = new NotSexType(1, "Male")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Male"),
        new TranslationName(LaguageType.English, "Male"),
        new TranslationName(LaguageType.Russian, "Мужской")
      }
    };

    public static NotSexType Female { get; } = new NotSexType(2, "Female")
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

    private NotSexType(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<NotSexType> List()
    {
      return new List<NotSexType> {None, Male, Female};
    }

    public static NotSexType FromKey(string key)
    {
      return FromString(key, List());
    }

    public static NotSexType FromValue(int id)
    {
      return FromValue(id, List());
    }

    public static bool IsValid(int id)
    {
      return IsValid(id, List());
    }

    public static implicit operator NotSexType(int x)
    {
      return List().FirstOrDefault(item => item.Id == x) ??
             throw new InvalidCastException($"Cannot cast int x:{x} to enumeration {nameof(NotSexType)}");
    }

    public static explicit operator int(NotSexType sexType)
    {
      return sexType.Id;
    }
  }
}