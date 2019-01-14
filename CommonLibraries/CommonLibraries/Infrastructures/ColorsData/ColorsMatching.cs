using System.Collections.Generic;
using CommonLibraries.CommonTypes;

namespace CommonLibraries.Infrastructures.ColorsData
{
  public class ColorsMatching
  {
    public ColorsGroups Autumn { get; set; }
    public ColorsGroups Spring { get; set; }
    public ColorsGroups Summer { get; set; }
    public ColorsGroups Winter { get; set; }
  }

  public class ColorsGroups
  {
    public ColorsGroup RedPink { get; set; }
    public ColorsGroup OrangeYellow { get; set; }
    public ColorsGroup Green { get; set; }
    public ColorsGroup Blue { get; set; }
    public ColorsGroup Purple { get; set; }
    public ColorsGroup BrownBeige { get; set; }
    public ColorsGroup GrayBlackWhite { get; set; }
  }

  public class ColorsGroup
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<BaseColor> BaseColors { get; set; }
    public List<ServerColor> GoodColors { get; set; }
    public List<ServerColor> BadColors { get; set; }
  }

  public class BaseColor
  {
    public BaseColorType ColorType { get; set; }
    public ServerColor Color { get; set; }
  }
}