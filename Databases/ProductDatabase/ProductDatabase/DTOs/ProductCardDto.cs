using System.Collections.Generic;
using ProductDatabase.Entities;

namespace ProductDatabase.DTOs
{
  public class ProductCardDto
  {
    public BrandEntity Brand { get; set; }
    public ProductEntity Product { get; set; }
    public IEnumerable<ProductPhotoEntity> ProductPhotos { get; set; }
    public IEnumerable<SizeTypeEntity> Sizes { get; set; }
    public double Goodness { get; set; }
  }
}