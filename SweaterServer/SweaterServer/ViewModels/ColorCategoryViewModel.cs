using System.Collections.Generic;

namespace SweaterServer.ViewModels
{
  public class ColorCategoryViewModel
  {
    public ColorViewModel RedPink { get; set; }
    public ColorViewModel OrangeYellow { get; set; }
    public ColorViewModel Green { get; set; }
    public ColorViewModel Blue { get; set; }
    public ColorViewModel Purple { get; set; }
    public ColorViewModel BrownBeige { get; set; }
    public ColorViewModel GrayBlackWhite { get; set; }
  }

  public class ColorViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Hexes { get; set; }
  }
}