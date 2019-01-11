using System.Collections.Generic;
using CommonLibraries.Infrastructures;

namespace CommonLibraries.CommonTypes
{
  public class BaseColorType : CustomEnum
  {
    public string Hex { get; set; }

    public static BaseColorType None { get; } = new BaseColorType(0, "None", "000000");
    public static BaseColorType Black { get; } = new BaseColorType(1, "Black", "000000");
    public static BaseColorType White { get; } = new BaseColorType(2, "White", "ffffff");
    public static BaseColorType Red { get; } = new BaseColorType(3, "Red", "ff0000");
    public static BaseColorType Pink { get; } = new BaseColorType(4, "Pink", "ffffff");
    public static BaseColorType Orange { get; } = new BaseColorType(5, "Orange", "ffa500");
    public static BaseColorType Yellow { get; } = new BaseColorType(6, "Yellow", "ffff00");
    public static BaseColorType Green { get; } = new BaseColorType(7, "Green", "008000");
    public static BaseColorType Blue { get; } = new BaseColorType(8, "Blue", "42aaff");
    public static BaseColorType Purple { get; } = new BaseColorType(9, "Purple", "800080");
    public static BaseColorType Brown { get; } = new BaseColorType(10, "Brown", "964b00");
    public static BaseColorType Beige { get; } = new BaseColorType(11, "Beige", "f5f5dc");
    public static BaseColorType Gray { get; } = new BaseColorType(12, "Gray", "808080");

    public BaseColorType(int id, string name, string hex) : base(id, name)
    {
      Hex = hex;
    }

    public static List<BaseColorType> AsList()
    {
      return new List<BaseColorType>
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

    public static explicit operator BaseColorType(int id)
    {
      return AsList().Find(x => x.Id == id);
    }
  }
}