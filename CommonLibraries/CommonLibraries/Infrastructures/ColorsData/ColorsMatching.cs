using System.Collections.Generic;
using System.Linq;
using CommonLibraries.CommonTypes;
using CommonLibraries.Resources.DeserializerTypes;

namespace CommonLibraries.Infrastructures.ColorsData
{
  public class ColorMatching
  {
    public ColorGroups Autumn { get; set; }
    public ColorGroups Spring { get; set; }
    public ColorGroups Summer { get; set; }
    public ColorGroups Winter { get; set; }


    public static ColorMatching FromColorsMatchingDeserializer(ColorMatchingDeserializer deserializer)
    {
      ColorGroup ToColorGroupDeserializer(ColorGroupDeserializer colorGroupDeserializer)
      {
        var colorGroup = new ColorGroup
        {
          Id = colorGroupDeserializer.Id,
          Name = colorGroupDeserializer.Name,
          BaseColors =
            colorGroupDeserializer.BaseColors.Select(x => new BaseColor
            {
              ColorType = (BaseColorType)x.Id,
              Color = new ServerColor(((BaseColorType)x.Id).Hex)
            }).ToList(),
          GoodColors = colorGroupDeserializer.GoodColors.Select(x => new ServerColor(x)).ToList(),
          BadColors = colorGroupDeserializer.BadColors.Select(x => new ServerColor(x)).ToList()
        };
        return colorGroup;
      }

      ColorGroups ToColorGroupsDeserializer(ColorGroupsDeserializer colorGroupsDeserializer)
      {
        var colorGroups = new ColorGroups
        {
          Blue = ToColorGroupDeserializer(colorGroupsDeserializer.Blue),
          BrownBeige = ToColorGroupDeserializer(colorGroupsDeserializer.BrownBeige),
          GrayBlackWhite = ToColorGroupDeserializer(colorGroupsDeserializer.GrayBlackWhite),
          Green = ToColorGroupDeserializer(colorGroupsDeserializer.Green),
          OrangeYellow = ToColorGroupDeserializer(colorGroupsDeserializer.OrangeYellow),
          Purple = ToColorGroupDeserializer(colorGroupsDeserializer.Purple),
          RedPink = ToColorGroupDeserializer(colorGroupsDeserializer.RedPink)
        };
        return colorGroups;
      }

      var result = new ColorMatching
      {
        Autumn = ToColorGroupsDeserializer(deserializer.Autumn),
        Spring = ToColorGroupsDeserializer(deserializer.Spring),
        Summer = ToColorGroupsDeserializer(deserializer.Summer),
        Winter = ToColorGroupsDeserializer(deserializer.Winter)
      };
      return result;
    }
  }

  public class ColorGroups
  {
    public ColorGroup RedPink { get; set; }
    public ColorGroup OrangeYellow { get; set; }
    public ColorGroup Green { get; set; }
    public ColorGroup Blue { get; set; }
    public ColorGroup Purple { get; set; }
    public ColorGroup BrownBeige { get; set; }
    public ColorGroup GrayBlackWhite { get; set; }
  }

  public class ColorGroup
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