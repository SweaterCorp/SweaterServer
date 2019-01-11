using System.Collections.Generic;

namespace CommonLibraries.Resources.DeserializerTypes
{
  public class DeserializerColorsMatching
  {
    public DeserializerColorsGroups Autumn { get; set; }
    public DeserializerColorsGroups Spring { get; set; }
    public DeserializerColorsGroups Summer { get; set; }
    public DeserializerColorsGroups Winter { get; set; }
  }

  public class DeserializerColorsGroups
  {
    public DeserializerColorsGroup RedPink { get; set; }
    public DeserializerColorsGroup OrangeYellow { get; set; }
    public DeserializerColorsGroup Green { get; set; }
    public DeserializerColorsGroup Blue { get; set; }
    public DeserializerColorsGroup Purple { get; set; }
    public DeserializerColorsGroup BrownBeige { get; set; }
    public DeserializerColorsGroup GrayBlackWhite { get; set; }
  }

  public class DeserializerColorsGroup
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<DeserializerColorType> BaseColors { get; set; }
    public List<string> GoodColors { get; set; }
    public List<string> BadColors { get; set; }
  }

  public class DeserializerColorType
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Hex { get; set; }
  }
}
