using System.Collections.Generic;

namespace CommonLibraries.Resources.DeserializerTypes
{
  public class DeserializerLamodaColors
  {
    public List<DeserializerColorCollection> Colors { get; set; }
  }

  public class DeserializerColorCollection
  {
    public DeserializerLamodaColorType LamodaColorType { get; set; }
    public List<string> HexColors { get; set; }
  }

  public class DeserializerLamodaColorType
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
}