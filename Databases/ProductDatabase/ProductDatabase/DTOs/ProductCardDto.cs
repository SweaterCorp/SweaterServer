using System.Collections.Generic;
using Newtonsoft.Json;
using ProductDatabase.Entities;

namespace ProductDatabase.DTOs
{
  public class ProductCardDto
  {
    public BrandEntity Brand { get; set; }
    public ProductEntity Product { get; set; }
    public List<SizeTypeEntity> Sizes { get; set; }
    public double Goodness { get; set; }
  }
}
