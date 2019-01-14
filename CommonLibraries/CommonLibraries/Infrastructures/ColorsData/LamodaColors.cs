using System.Collections.Generic;
using CommonLibraries.CommonTypes;

namespace CommonLibraries.Infrastructures.ColorsData
{
  public class LamodaColors
  {
    public List<ColorCollection> Colors { get; set; }
  }

  public class ColorCollection
  {
    public LamodaColorType LamodaColorType { get; set; }
    public List<ServerColor> ServerColors { get; set; }
  }
}