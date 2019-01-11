using System.Collections.Generic;
using ProductDatabase.Entities;

namespace ProductDatabase.DTOs
{
  public class ProductCardDto
  {
    public BrandEntity Brand { get; set; }
    public ProductEntity Product { get; set; }
    public List<SizeTypeEntity> Sizes { get; set; }
    public int Color { get; set; }
  }
}
