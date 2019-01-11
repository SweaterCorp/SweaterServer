using System.Collections.Generic;
using CommonLibraries.Infrastructures;

namespace CommonLibraries.CommonTypes
{
  public class CategoryType : CustomEnum
  {
    public static CategoryType None { get; } = new CategoryType(0, "None");
    public static CategoryType BlousesShirts { get; } = new CategoryType(1, "BlousesShirts");
    public static CategoryType Trousers { get; } = new CategoryType(2, "Trousers");
    public static CategoryType Outerwear { get; } = new CategoryType(3, "Outerwear");
    public static CategoryType JumpersSweatersCardigans { get; } = new CategoryType(4, "JumpersSweatersCardigans");
    public static CategoryType Jeans { get; } = new CategoryType(5, "Jeans");
    public static CategoryType JacketsSuits { get; } = new CategoryType(6, "JacketsSuits");
    public static CategoryType Dresses { get; } = new CategoryType(7, "Dresses");
    public static CategoryType Tracksuits { get; } = new CategoryType(8, "Tracksuits");
    public static CategoryType HoodiesSweatshirts { get; } = new CategoryType(9, "HoodiesSweatshirts");
    public static CategoryType Tops { get; } = new CategoryType(10, "Tops");
    public static CategoryType TshirtsPolo { get; } = new CategoryType(11, "TShirtsPolo");
    public static CategoryType Shorts { get; } = new CategoryType(12, "Shorts");
    public static CategoryType Skirts { get; } = new CategoryType(13, "Skirts");

    private CategoryType(int id, string name) : base(id, name)
    {
    }

    public static List<CategoryType> AsList()
    {
      return new List<CategoryType>
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

    public static explicit operator CategoryType(int id)
    {
      return AsList().Find(x => x.Id == id);
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
}