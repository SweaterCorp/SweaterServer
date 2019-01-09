using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibraries.Infrastructures;
using CommonLibraries.Localization;

namespace CommonLibraries.Old
{
  public class NotCategory : NotEnumeration
  {
    private List<TranslationName> _translationNames = new List<TranslationName>();

    // ------------------------ 

    public static NotCategory None { get; } = new NotCategory(0, "None")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "None"),
        new TranslationName(LaguageType.English, "None"),
        new TranslationName(LaguageType.Russian, "Неизвестно")
      }
    };

    // ------------------------ 

    public static NotCategory BlousesShirts { get; } = new NotCategory(1, "BlousesShirts")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Blouses and shirts"),
        new TranslationName(LaguageType.English, "Blouses and shirts"),
        new TranslationName(LaguageType.Russian, "Блузки и рубашки")
      }
    };

    public static NotCategory Trousers { get; } = new NotCategory(2, "Trousers")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Trousers"),
        new TranslationName(LaguageType.English, "Trousers"),
        new TranslationName(LaguageType.Russian, "Штаны")
      }
    };

    public static NotCategory Outerwear { get; } = new NotCategory(3, "Outerwear")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Outerwear"),
        new TranslationName(LaguageType.English, "Outerwear"),
        new TranslationName(LaguageType.Russian, "Верхняя одежда")
      }
    };

    public static NotCategory JumpersSweatersCardigans { get; } = new NotCategory(4, "JumpersSweatersCardigans")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Jumpers, sweaters, and cardigans"),
        new TranslationName(LaguageType.English, "Jumpers, sweaters, and cardigans"),
        new TranslationName(LaguageType.Russian, "Джемперы, свитеры и кардиганы")
      }
    };

    public static NotCategory Jeans { get; } = new NotCategory(5, "Jeans")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Jeans"),
        new TranslationName(LaguageType.English, "Jeans"),
        new TranslationName(LaguageType.Russian, "Джинсы")
      }
    };

    public static NotCategory JacketsSuits { get; } = new NotCategory(6, "JacketsSuits")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Jackets and suits"),
        new TranslationName(LaguageType.English, "Jackets and suits"),
        new TranslationName(LaguageType.Russian, "Пиджаки и жакеты")
      }
    };

    public static NotCategory Dresses { get; } = new NotCategory(7, "Dresses")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Dresses"),
        new TranslationName(LaguageType.English, "Dresses"),
        new TranslationName(LaguageType.Russian, "Платья")
      }
    };

    public static NotCategory Tracksuits { get; } = new NotCategory(8, "Tracksuits")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Tracksuits"),
        new TranslationName(LaguageType.English, "Tracksuits"),
        new TranslationName(LaguageType.Russian, "Спортивные костюмы")
      }
    };

    public static NotCategory HoodiesSweatshirts { get; } = new NotCategory(9, "HoodiesSweatshirts")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Hoodies and sweatshirts"),
        new TranslationName(LaguageType.English, "Hoodies and hweatshirts"),
        new TranslationName(LaguageType.Russian, "Толстовки и олимпийки")
      }
    };

    public static NotCategory Tops { get; } = new NotCategory(10, "Tops")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Tops"),
        new TranslationName(LaguageType.English, "Tops"),
        new TranslationName(LaguageType.Russian, "Топы")
      }
    };

    public static NotCategory TshirtsPolo { get; } = new NotCategory(11, "TShirtsPolo")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "T-Shirts and polo"),
        new TranslationName(LaguageType.English, "T-Shirts and polo"),
        new TranslationName(LaguageType.Russian, "Футболки и поло")
      }
    };

    public static NotCategory Shorts { get; } = new NotCategory(12, "Shorts")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Shorts"),
        new TranslationName(LaguageType.English, "Shorts"),
        new TranslationName(LaguageType.Russian, "Шорты")
      }
    };

    public static NotCategory Skirts { get; } = new NotCategory(13, "Skirts")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Skirts"),
        new TranslationName(LaguageType.English, "Skirts"),
        new TranslationName(LaguageType.Russian, "Юбки")
      }
    };

    // ------------------ 

    public string this[LaguageType laguageType] => _translationNames.FirstOrDefault(x => x.LaguageType == laguageType)
                                                     ?.Name ?? _translationNames[0].Name;

    private NotCategory(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<NotCategory> List()
    {
      return new List<NotCategory>
      {
        None,
        BlousesShirts,
        Trousers,
        Outerwear,
        JumpersSweatersCardigans,
        Jeans,
        JacketsSuits,
        Dresses,
        Tracksuits,
        HoodiesSweatshirts,
        Tops,
        TshirtsPolo,
        Shorts,
        Skirts
      };
    }

    public static NotCategory FromKey(string key)
    {
      return FromString(key, List());
    }

    public static NotCategory FromValue(int id)
    {
      return FromValue(id, List());
    }

    public static bool IsValid(int id)
    {
      return IsValid(id, List());
    }

    public static implicit operator NotCategory(int x)
    {
      return List().FirstOrDefault(item => item.Id == x) ??
             throw new InvalidCastException($"Cannot cast int x:{x} to enumeration {nameof(NotCategory)}");
    }

    public static explicit operator int(NotCategory humanColorType)
    {
      return humanColorType.Id;
    }
  }

  //("blouses_shirts","https://www.lamoda.ru/c/399/clothes-bluzy-rubashki/"),
  //("trousers","https://www.lamoda.ru/c/401/clothes-bryuki-shorty-kombinezony/"),
  //("outerwear","https://www.lamoda.ru/c/357/clothes-verkhnyaya-odezhda/"),
  //("jumpers_sweaters_cardigans","https://www.lamoda.ru/c/371/clothes-trikotazh/"),
  //("jeans","https://www.lamoda.ru/c/397/clothes-d-insy/"),
  //("jackets_suits","https://www.lamoda.ru/c/367/clothes-pidzhaki-zhaketi/"),
  //("dresses","https://www.lamoda.ru/c/369/clothes-platiya/"),
  //("tracksuits","https://www.lamoda.ru/c/415/clothes-kostyumy/"),
  //("hoodies_sweatshirts","https://www.lamoda.ru/c/2474/clothes-tolstovki-olimpiyki/"),
  //("tops","https://www.lamoda.ru/c/2627/clothes-topy/"),
  //("t_shirts_polo","https://www.lamoda.ru/c/2478/clothes-futbolki/"),
  //("shorts","https://www.lamoda.ru/c/2485/clothes-shorty/"),
  //("skirts","https://www.lamoda.ru/c/423/clothes-yubki/")
  //]
}