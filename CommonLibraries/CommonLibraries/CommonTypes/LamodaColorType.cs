using System.Collections.Generic;
using CommonLibraries.Infrastructures;

namespace CommonLibraries.CommonTypes
{
  public class LamodaColorType : CustomEnum
  {
    public string RussianName { get; set; }

    public static LamodaColorType None { get; } = new LamodaColorType(0, "None", "None");
    public static LamodaColorType Beige { get; } = new LamodaColorType(1, "Beige", "Бежевый");
    public static LamodaColorType Black { get; } = new LamodaColorType(2, "Black", "Черный");
    public static LamodaColorType Blue { get; } = new LamodaColorType(3, "Blue ", "Синий");
    public static LamodaColorType Brown { get; } = new LamodaColorType(4, "Brown", "Коричневый");
    public static LamodaColorType Burgundy { get; } = new LamodaColorType(5, "Burgundy ", "Бордовый");
    public static LamodaColorType Coral { get; } = new LamodaColorType(6, "Coral ", "Кораловый");
    public static LamodaColorType Cyan { get; } = new LamodaColorType(7, "Cyan ", "Голубой");
    public static LamodaColorType Gold { get; } = new LamodaColorType(8, "Gold", "Зотолой");
    public static LamodaColorType Gray { get; } = new LamodaColorType(9, "Gray", "Серый");
    public static LamodaColorType Green { get; } = new LamodaColorType(10, "Green", "Зеленый");
    public static LamodaColorType Khaki { get; } = new LamodaColorType(11, "Khaki", "Хаки");
    public static LamodaColorType Multicolor { get; } = new LamodaColorType(12, "Multicolor ", "Мультиколор");
    public static LamodaColorType Orange { get; } = new LamodaColorType(13, "Orange ", "Оранжевый");
    public static LamodaColorType Pink { get; } = new LamodaColorType(14, "Pink ", "Розовый");
    public static LamodaColorType Purple { get; } = new LamodaColorType(15, "Purple ", "Фиолетовый");
    public static LamodaColorType Red { get; } = new LamodaColorType(16, "Red ", "Красный");
    public static LamodaColorType Silver { get; } = new LamodaColorType(17, "Silver ", "Серебряный");
    public static LamodaColorType Transparent { get; } = new LamodaColorType(18, "Transparent", "Прозрачный");
    public static LamodaColorType Turquoise { get; } = new LamodaColorType(19, "Turquoise ", "Бирюзовый");
    public static LamodaColorType White { get; } = new LamodaColorType(20, "White", "Белый");
    public static LamodaColorType Yellow { get; } = new LamodaColorType(21, "Yellow", "Желтый");

    public LamodaColorType(int id, string name, string russianName = "") : base(id, name)
    {
      RussianName = russianName;
    }

    public static List<LamodaColorType> AsList()
    {
      return new List<LamodaColorType>
      {
        None,
        Beige,
        White,
        Turquoise,
        Burgundy,
        Cyan,
        Yellow,
        Green,
        Gold,
        Coral,
        Brown,
        Red,
        Multicolor,
        Orange,
        Transparent,
        Pink,
        Silver,
        Gray,
        Blue,
        Purple,
        Khaki,
        Black
      };
    }

    public static explicit operator LamodaColorType(int id)
    {
      return AsList().Find(x => x.Id == id);
    }

    public static explicit operator LamodaColorType(string name)
    {
      return AsList().Find(x => x.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
    }

    public static LamodaColorType StringToLamodaColorType(string name, bool isRussian = false)
    {
      return isRussian ? AsList().Find(x => x.RussianName.Equals(name, System.StringComparison.OrdinalIgnoreCase)) : (LamodaColorType)name;
    }
  }
}