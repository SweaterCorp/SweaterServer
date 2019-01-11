using System.Collections.Generic;
using CommonLibraries.CommonTypes;
using CommonLibraries.Infrastructures;

namespace ZebraMain.Infrastructure.ColorsData
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