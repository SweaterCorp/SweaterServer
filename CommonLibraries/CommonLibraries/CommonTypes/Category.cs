using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibraries.Infrastructures;
using CommonLibraries.Localization;

namespace CommonLibraries.CommonTypes
{
  public class Category : Enumeration
  {
    private List<TranslationName> _translationNames = new List<TranslationName>();

    // ------------------------ 

    public static Category None { get; } = new Category(0, "None")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "None"),
        new TranslationName(LaguageType.English, "None"),
        new TranslationName(LaguageType.Russian, "Неизвестно")
      }
    };

    // ------------------------ 

    public static Category BlousesShirts { get; } = new Category(1, "BlousesShirts")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Blouses and shirts"),
        new TranslationName(LaguageType.English, "Blouses and shirts"),
        new TranslationName(LaguageType.Russian, "Блузки и рубашки")
      }
    };

    public static Category Trousers { get; } = new Category(2, "Trousers")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Trousers"),
        new TranslationName(LaguageType.English, "Trousers"),
        new TranslationName(LaguageType.Russian, "Штаны")
      }
    };

    public static Category Outerwear { get; } = new Category(3, "Outerwear")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Outerwear"),
        new TranslationName(LaguageType.English, "Outerwear"),
        new TranslationName(LaguageType.Russian, "Верхняя одежда")
      }
    };

    public static Category JumpersSweatersCardigans { get; } = new Category(4, "JumpersSweatersCardigans")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Jumpers, sweaters, and cardigans"),
        new TranslationName(LaguageType.English, "Jumpers, sweaters, and cardigans"),
        new TranslationName(LaguageType.Russian, "Джемперы, свитеры и кардиганы")
      }
    };

    public static Category Jeans { get; } = new Category(5, "Jeans")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Jeans"),
        new TranslationName(LaguageType.English, "Jeans"),
        new TranslationName(LaguageType.Russian, "Джинсы")
      }
    };

    public static Category JacketsSuits { get; } = new Category(6, "JacketsSuits")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Jackets and suits"),
        new TranslationName(LaguageType.English, "Jackets and suits"),
        new TranslationName(LaguageType.Russian, "Пиджаки и жакеты")
      }
    };

    public static Category Dresses { get; } = new Category(7, "Dresses")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Dresses"),
        new TranslationName(LaguageType.English, "Dresses"),
        new TranslationName(LaguageType.Russian, "Платья")
      }
    };

    public static Category Tracksuits { get; } = new Category(8, "Tracksuits")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Tracksuits"),
        new TranslationName(LaguageType.English, "Tracksuits"),
        new TranslationName(LaguageType.Russian, "Спортивные костюмы")
      }
    };

    public static Category HoodiesSweatshirts { get; } = new Category(9, "HoodiesSweatshirts")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Hoodies and sweatshirts"),
        new TranslationName(LaguageType.English, "Hoodies and hweatshirts"),
        new TranslationName(LaguageType.Russian, "Толстовки и олимпийки")
      }
    };

    public static Category Tops { get; } = new Category(10, "Tops")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Tops"),
        new TranslationName(LaguageType.English, "Tops"),
        new TranslationName(LaguageType.Russian, "Топы")
      }
    };

    public static Category TshirtsPolo { get; } = new Category(11, "TShirtsPolo")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "T-Shirts and polo"),
        new TranslationName(LaguageType.English, "T-Shirts and polo"),
        new TranslationName(LaguageType.Russian, "Футболки и поло")
      }
    };

    public static Category Shorts { get; } = new Category(12, "Shorts")
    {
      _translationNames = new List<TranslationName>
      {
        new TranslationName(LaguageType.Default, "Shorts"),
        new TranslationName(LaguageType.English, "Shorts"),
        new TranslationName(LaguageType.Russian, "Шорты")
      }
    };

    public static Category Skirts { get; } = new Category(13, "Skirts")
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

    private Category(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<Category> List()
    {
      return new List<Category>
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

    public static Category FromKey(string key)
    {
      return FromString(key, List());
    }

    public static Category FromValue(int id)
    {
      return FromValue(id, List());
    }

    public static bool IsValid(int id)
    {
      return IsValid(id, List());
    }

    public static implicit operator Category(int x)
    {
      return List().FirstOrDefault(item => item.Id == x) ??
             throw new InvalidCastException($"Cannot cast int x:{x} to enumeration {nameof(Category)}");
    }

    public static explicit operator int(Category humanColorType)
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