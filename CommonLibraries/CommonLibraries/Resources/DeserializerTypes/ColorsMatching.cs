using System.Collections.Generic;

namespace CommonLibraries.Resources.DeserializerTypes
{
  public class ColorMatchingDeserializer
  {
    public ColorGroupsDeserializer Autumn { get; set; }
    public ColorGroupsDeserializer Spring { get; set; }
    public ColorGroupsDeserializer Summer { get; set; }
    public ColorGroupsDeserializer Winter { get; set; }
  }

  public class ColorGroupsDeserializer
  {
    public ColorGroupDeserializer RedPink { get; set; }
    public ColorGroupDeserializer OrangeYellow { get; set; }
    public ColorGroupDeserializer Green { get; set; }
    public ColorGroupDeserializer Blue { get; set; }
    public ColorGroupDeserializer Purple { get; set; }
    public ColorGroupDeserializer BrownBeige { get; set; }
    public ColorGroupDeserializer GrayBlackWhite { get; set; }
  }

  public class ColorGroupDeserializer
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ColorTypeDeserializer> BaseColors { get; set; }
    public List<string> GoodColors { get; set; }
    public List<string> BadColors { get; set; }
  }

  public class ColorTypeDeserializer
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Hex { get; set; }
  }
}
