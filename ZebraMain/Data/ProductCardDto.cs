using System.Collections.Generic;
using ZebraData.Entities.ProductGroup;

namespace ZebraData
{
  public class ProductCardDto
  {
    public BrandEntity Brand { get; set; }
    public ProductEntity Product { get; set; }
    public List<SizeTypeEntity> Sizes { get; set; }
    public List<ColorTypeEntity> Colors { get; set; }
  }
}
