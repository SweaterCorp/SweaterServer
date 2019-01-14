using System.Collections.Generic;

namespace CommonLibraries.Resources.DeserializerTypes
{
  public class LamodaColorsDeserializer
  {
    public List<ColorCollectionDeserializer> Colors { get; set; }
  }

  public class ColorCollectionDeserializer
  {
    public int LamodaColorType { get; set; }
    public List<string> HexColors { get; set; }
  }

  public class LamodaColorTypeDeserializer
  {
    public int Id { get; set; }

  }
}