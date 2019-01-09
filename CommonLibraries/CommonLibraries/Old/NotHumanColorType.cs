using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibraries.Infrastructures;
using CommonLibraries.Localization;

namespace CommonLibraries.Old
{
  public class NotHumanColorType : NotEnumeration
  {
    private List<TranslationName> _translationNames = new List<TranslationName>();

    // ------------------------ Зима

    public static NotHumanColorType None { get; } = new NotHumanColorType(0, "None")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "None"),
        new TranslationName(LaguageType.English, "None"),
        new TranslationName(LaguageType.Russian, "Неизвестно")
      }
    };

    // ------------------------ Зима

    public static NotHumanColorType DarkWinter { get; } = new NotHumanColorType(1, "DarkWinter")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Dark winter"),
        new TranslationName(LaguageType.English, "Dark winter"),
        new TranslationName(LaguageType.Russian, "Темная зима")
      }
    };

    public static NotHumanColorType ColdWinter { get; } = new NotHumanColorType(2, "ColdWinter")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Cold winter"),
        new TranslationName(LaguageType.English, "Cold winter"),
        new TranslationName(LaguageType.Russian, "Холодная зима")
      }
    };

    public static NotHumanColorType BrightWinter { get; } = new NotHumanColorType(3, "BrightWinter")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Bright winter"),
        new TranslationName(LaguageType.English, "Bright winter"),
        new TranslationName(LaguageType.Russian, "Яркая зима")
      }
    };

    // ------------------------ Осень

    public static NotHumanColorType SoftAutumn { get; } = new NotHumanColorType(4, "SoftAutumn")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Soft Autumn"),
        new TranslationName(LaguageType.English, "Soft Autumn"),
        new TranslationName(LaguageType.Russian, "Мягкая осень")
      }
    };

    public static NotHumanColorType WarmAutumn { get; } = new NotHumanColorType(5, "WarmAutumn")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Warm autumn"),
        new TranslationName(LaguageType.English, "Warm autumn"),
        new TranslationName(LaguageType.Russian, "Теплая осень")
      }
    };

    public static NotHumanColorType DarkAutumn { get; } = new NotHumanColorType(6, "DarkAutumn")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Dark autumn"),
        new TranslationName(LaguageType.English, "Dark autumn"),
        new TranslationName(LaguageType.Russian, "Темная осень")
      }
    };

    // ------------------------ Весна

    public static NotHumanColorType BrightSpring { get; } = new NotHumanColorType(7, "BrightSpring")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Bright spring"),
        new TranslationName(LaguageType.English, "Bright spring"),
        new TranslationName(LaguageType.Russian, "Яркая весна")
      }
    };

    public static NotHumanColorType WarmSpring { get; } = new NotHumanColorType(8, "WarmSpring")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Warm spring"),
        new TranslationName(LaguageType.English, "Warm spring"),
        new TranslationName(LaguageType.Russian, "Мягкая осень")
      }
    };

    public static NotHumanColorType LightSpring { get; } = new NotHumanColorType(9, "LightSpring")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Light spring"),
        new TranslationName(LaguageType.English, "Light spring"),
        new TranslationName(LaguageType.Russian, "Светлая весна")
      }
    };

    // ------------------------ Лето

    public static NotHumanColorType LightSummer { get; } = new NotHumanColorType(10, "LightSummer")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Light summer"),
        new TranslationName(LaguageType.English, "Light summer"),
        new TranslationName(LaguageType.Russian, "Светлое лето")
      }
    };

    public static NotHumanColorType ColdSummer { get; } = new NotHumanColorType(11, "ColdSummer")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Cold summer"),
        new TranslationName(LaguageType.English, "Cold summer"),
        new TranslationName(LaguageType.Russian, "Холодное лето")
      }
    };

    public static NotHumanColorType SoftSummer { get; } = new NotHumanColorType(12, "SoftSummer")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Soft summer"),
        new TranslationName(LaguageType.English, "Soft summer"),
        new TranslationName(LaguageType.Russian, "Мягкое лето")
      }
    };

    // ------------------ 

    public string this[LaguageType laguageType] => _translationNames.FirstOrDefault(x => x.LaguageType == laguageType)
                                                     ?.Name ?? _translationNames[0].Name;

    private NotHumanColorType(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<NotHumanColorType> List()
    {
      return new List<NotHumanColorType>
      {
        None,
        DarkWinter,
        ColdWinter,
        BrightWinter,
        SoftAutumn,
        WarmAutumn,
        DarkAutumn,
        BrightSpring,
        WarmSpring,
        LightSpring,
        LightSummer,
        ColdSummer,
        SoftSummer
      };
    }

    public static NotHumanColorType FromKey(string key)
    {
      return FromString(key, List());
    }

    public static NotHumanColorType FromValue(int id)
    {
      return FromValue(id, List());
    }

    public static bool IsValid(int id)
    {
      return IsValid(id, List());
    }

    public static implicit operator NotHumanColorType(int x)
    {
      return List().FirstOrDefault(item => item.Id == x) ??
             throw new InvalidCastException($"Cannot cast int x:{x} to enumeration {nameof(NotHumanColorType)}");
    }

    public static explicit operator int(NotHumanColorType humanColorType)
    {
      return humanColorType.Id;
    }
  }
}