using System.Collections.Generic;
using CommonLibraries.Infrastructures;

namespace CommonLibraries.CommonTypes
{
  public class ColorGroupType : CustomEnum
  {
    public static ColorGroupType None { get; } = new ColorGroupType(0, "None");
    public static ColorGroupType RedPink { get; set; } = new ColorGroupType(1, "RedPink");
    public static ColorGroupType OrangeYellow { get; set; } = new ColorGroupType(2, "OrangeYellow");
    public static ColorGroupType Green { get; set; } = new ColorGroupType(3, "Green");
    public static ColorGroupType Blue { get; set; } = new ColorGroupType(4, "Blue");
    public static ColorGroupType Purple { get; set; } = new ColorGroupType(5, "Purple");
    public static ColorGroupType BrownBeige { get; set; } = new ColorGroupType(6, "BrownBeige");
    public static ColorGroupType GrayBlackWhite { get; set; } = new ColorGroupType(7, "GrayBlackWhite");

    public ColorGroupType(int id, string name) : base(id, name)
    {
    }

    public static List<ColorGroupType> AsList()
    {
      return new List<ColorGroupType> {None, RedPink, OrangeYellow, Green, Blue, Purple, BrownBeige, GrayBlackWhite};
    }

    public static explicit operator ColorGroupType(int id)
    {
      return AsList().Find(x => x.Id == id);
    }
  }
}