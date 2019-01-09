using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibraries.Infrastructures;
using CommonLibraries.Localization;

namespace CommonLibraries.Old
{
  public class NotColorType : NotEnumeration
  {
    private List<TranslationName> _translationNames = new List<TranslationName>();

    public static NotColorType None { get; } = new NotColorType(0, "None", "000000")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "None"),
        new TranslationName(LaguageType.English, "None"),
        new TranslationName(LaguageType.Russian, "Неизвестно")
      }
    };

    // ------------

    public static NotColorType Black { get; } = new NotColorType(1, "Black", "000000")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Black"),
        new TranslationName(LaguageType.English, "Black"),
        new TranslationName(LaguageType.Russian, "Черный")
      }
    };

    public static NotColorType White { get; } = new NotColorType(2, "White", "ffffff")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "White"),
        new TranslationName(LaguageType.English, "White"),
        new TranslationName(LaguageType.Russian, "Белый")
      }
    };

    // -----------

    public static NotColorType Red { get; } = new NotColorType(3, "Red", "ff0000")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Red"),
        new TranslationName(LaguageType.English, "Red"),
        new TranslationName(LaguageType.Russian, "Красный")
      }
    };

    public static NotColorType Pink { get; } = new NotColorType(4, "Pink", "ffffff")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Pink"),
        new TranslationName(LaguageType.English, "Pink"),
        new TranslationName(LaguageType.Russian, "Розовый")
      }
    };

    public static NotColorType Orange { get; } = new NotColorType(5, "Orange", "ffa500")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Orange"),
        new TranslationName(LaguageType.English, "Orange"),
        new TranslationName(LaguageType.Russian, "Оранжевый")
      }
    };

    public static NotColorType Yellow { get; } = new NotColorType(6, "Yellow", "ffff00")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Yellow"),
        new TranslationName(LaguageType.English, "Yellow"),
        new TranslationName(LaguageType.Russian, "Желтый")
      }
    };

    public static NotColorType Green { get; } = new NotColorType(7, "Green", "008000")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Green"),
        new TranslationName(LaguageType.English, "Green"),
        new TranslationName(LaguageType.Russian, "Зеленый")
      }
    };

    public static NotColorType Blue { get; } = new NotColorType(8, "Blue", "42aaff")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Blue"),
        new TranslationName(LaguageType.English, "Blue"),
        new TranslationName(LaguageType.Russian, "Голубой")
      }
    };

    public static NotColorType Purple { get; } = new NotColorType(9, "Purple", "800080")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Purple"),
        new TranslationName(LaguageType.English, "Purple"),
        new TranslationName(LaguageType.Russian, "Пурпурный")
      }
    };

    public static NotColorType Brown { get; } = new NotColorType(10, "Brown", "964b00")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Brown"),
        new TranslationName(LaguageType.English, "Brown"),
        new TranslationName(LaguageType.Russian, "Коричневый")
      }
    };

    public static NotColorType Beige { get; } = new NotColorType(11, "Beige", "f5f5dc")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Beige"),
        new TranslationName(LaguageType.English, "Beige"),
        new TranslationName(LaguageType.Russian, "Бежевый")
      }
    };

    public static NotColorType Gray { get; } = new NotColorType(12, "Gray", "808080")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Gray"),
        new TranslationName(LaguageType.English, "Gray"),
        new TranslationName(LaguageType.Russian, "Серый")
      }
    };

    public ServerColor ServerColor { get; }

    public string this[LaguageType laguageType] => _translationNames.FirstOrDefault(x => x.LaguageType == laguageType)
                                                     ?.Name ?? _translationNames[0].Name;

    private NotColorType(int id, string name, RGB rgb) : base(id, name)
    {
      ServerColor = new ServerColor(rgb);
    }

    private NotColorType(int id, string name, string hex) : base(id, name)
    {
      ServerColor = new ServerColor(hex);
    }

    public static IEnumerable<NotColorType> List()
    {
      return new List<NotColorType>
      {
        None,
        White,
        Black,
        Red,
        Pink,
        Orange,
        Yellow,
        Green,
        Blue,
        Purple,
        Brown,
        Beige,
        Gray
      };
    }

    public static NotColorType FromKey(string key)
    {
      return FromString(key, List());
    }

    public static NotColorType FromValue(int id)
    {
      return FromValue(id, List());
    }

    public static bool IsValid(int id)
    {
      return IsValid(id, List());
    }

    public static implicit operator NotColorType(int x)
    {
      return List().FirstOrDefault(item => item.Id == x) ??
             throw new InvalidCastException($"Cannot cast int x:{x} to enumeration {nameof(NotColorType)}");
    }

    public static explicit operator int(NotColorType serverColor)
    {
      return serverColor.Id;
    }
  }
}