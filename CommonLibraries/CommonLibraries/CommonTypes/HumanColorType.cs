using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibraries.Infrastructures;
using CommonLibraries.Localization;

namespace CommonLibraries.CommonTypes
{
  public class HumanColorType : Enumeration
  {
    private List<TranslationName> _translationNames = new List<TranslationName>();

    // ------------------------ Зима

    public static HumanColorType None { get; } = new HumanColorType(0, "None")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "None"),
        new TranslationName(LaguageType.English, "None"),
        new TranslationName(LaguageType.Russian, "Неизвестно")
      }
    };

    // ------------------------ Зима

    public static HumanColorType DarkWinter { get; } = new HumanColorType(1, "DarkWinter")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Dark winter"),
        new TranslationName(LaguageType.English, "Dark winter"),
        new TranslationName(LaguageType.Russian, "Темная зима")
      }
    };

    public static HumanColorType ColdWinter { get; } = new HumanColorType(2, "ColdWinter")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Cold winter"),
        new TranslationName(LaguageType.English, "Cold winter"),
        new TranslationName(LaguageType.Russian, "Холодная зима")
      }
    };

    public static HumanColorType BrightWinter { get; } = new HumanColorType(3, "BrightWinter")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Bright winter"),
        new TranslationName(LaguageType.English, "Bright winter"),
        new TranslationName(LaguageType.Russian, "Яркая зима")
      }
    };

    // ------------------------ Осень

    public static HumanColorType SoftAutumn { get; } = new HumanColorType(4, "SoftAutumn")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Soft Autumn"),
        new TranslationName(LaguageType.English, "Soft Autumn"),
        new TranslationName(LaguageType.Russian, "Мягкая осень")
      }
    };

    public static HumanColorType WarmAutumn { get; } = new HumanColorType(5, "WarmAutumn")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Warm autumn"),
        new TranslationName(LaguageType.English, "Warm autumn"),
        new TranslationName(LaguageType.Russian, "Теплая осень")
      }
    };

    public static HumanColorType DarkAutumn { get; } = new HumanColorType(6, "DarkAutumn")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Dark autumn"),
        new TranslationName(LaguageType.English, "Dark autumn"),
        new TranslationName(LaguageType.Russian, "Темная осень")
      }
    };

    // ------------------------ Весна

    public static HumanColorType BrightSpring { get; } = new HumanColorType(7, "BrightSpring")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Bright spring"),
        new TranslationName(LaguageType.English, "Bright spring"),
        new TranslationName(LaguageType.Russian, "Яркая весна")
      }
    };

    public static HumanColorType WarmSpring { get; } = new HumanColorType(8, "WarmSpring")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Warm spring"),
        new TranslationName(LaguageType.English, "Warm spring"),
        new TranslationName(LaguageType.Russian, "Мягкая осень")
      }
    };

    public static HumanColorType LightSpring { get; } = new HumanColorType(9, "LightSpring")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Light spring"),
        new TranslationName(LaguageType.English, "Light spring"),
        new TranslationName(LaguageType.Russian, "Светлая весна")
      }
    };

    // ------------------------ Лето

    public static HumanColorType LightSummer { get; } = new HumanColorType(10, "LightSummer")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Light summer"),
        new TranslationName(LaguageType.English, "Light summer"),
        new TranslationName(LaguageType.Russian, "Светлое лето")
      }
    };

    public static HumanColorType ColdSummer { get; } = new HumanColorType(11, "ColdSummer")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Cold summer"),
        new TranslationName(LaguageType.English, "Cold summer"),
        new TranslationName(LaguageType.Russian, "Холодное лето")
      }
    };

    public static HumanColorType SoftSummer { get; } = new HumanColorType(12, "SoftSummer")
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

    private HumanColorType(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<HumanColorType> List()
    {
      return new List<HumanColorType>
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

    public static HumanColorType FromKey(string key)
    {
      return FromString(key, List());
    }

    public static HumanColorType FromValue(int id)
    {
      return FromValue(id, List());
    }

    public static bool IsValid(int id)
    {
      return IsValid(id, List());
    }

    public static implicit operator HumanColorType(int x)
    {
      return List().FirstOrDefault(item => item.Id == x) ??
             throw new InvalidCastException($"Cannot cast int x:{x} to enumeration {nameof(HumanColorType)}");
    }

    public static explicit operator int(HumanColorType humanColorType)
    {
      return humanColorType.Id;
    }
  }
}