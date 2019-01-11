using System.Collections.Generic;
using CommonLibraries.Infrastructures;

namespace CommonLibraries.CommonTypes
{
  public class PrintType : CustomEnum
  {
    public string RussianName { get; set; }

    public static PrintType None { get; } = new PrintType(0, "None", "None");
    public static PrintType Geometry { get; } = new PrintType(1, "Geometry;", "Геометрия");
    public static PrintType PolkaDotted { get; } = new PrintType(2, "Polka-dotted", "Горох");
    public static PrintType Other { get; } = new PrintType(3, "Other", "Другое");
    public static PrintType Animals { get; } = new PrintType(4, "Animals", "Животные");
    public static PrintType Camouflage { get; } = new PrintType(5, "Camouflage", "Камуфляж");
    public static PrintType Checked { get; } = new PrintType(6, "Checked", "Клетка");
    public static PrintType Spotted { get; } = new PrintType(7, "Spotted", "Леопардовый");
    public static PrintType Plain { get; } = new PrintType(8, "Plain", "Однотонный");
    public static PrintType Striped { get; } = new PrintType(9, "Striped", "Полоска");
    public static PrintType Printed { get; } = new PrintType(10, "Printed", "Рисунки и надписи");
    public static PrintType Floral { get; } = new PrintType(11, "Floral", "Цветочный");

    private PrintType(int id, string name, string russianName = "") : base(id, name)
    {
      RussianName = russianName;
    }

    public static List<PrintType> AsList()
    {
      return new List<PrintType>
      {
        None,
        Geometry,
        PolkaDotted,
        Other,
        Animals,
        Camouflage,
        Checked,
        Spotted,
        Plain,
        Striped,
        Printed,
        Floral
      };
    }

    public static explicit operator PrintType(int id)
    {
      return AsList().Find(x => x.Id == id);
    }
  }
}