using System.Collections.Generic;
using System.Linq;
using ProductDatabase.DTOs;
using SweaterMain.Infrastructure;

namespace SweaterMain.ViewModels
{
  public class ProductViewModel
  {
    public int ProductId { get; set; }
    public string VendorCode { get; set; }
    public decimal Price { get; set; }
    public string Link { get; set; }
    public List<string> Photos { get; set; }

    public BrandViewModel Brand { get; set; }
    public List<SizeViewModel> Sizes { get; set; }
    public double Goodness { get; set; }

    public static ProductViewModel FromPrdouctDto(ProductCardDto dto)
    {
      var result = new ProductViewModel
      {
        ProductId = dto.Product.ProductId,
        Brand = new BrandViewModel {BrandId = dto.Brand.BrandId, Name = dto.Brand.Name},
        Goodness = dto.Goodness,
        Link = dto.Product.Link,
        Price = dto.Product.Price,
        VendorCode = dto.Product.VendorCode,
        Sizes = dto.Sizes.Select(x => new SizeViewModel {SizeTypeId = x.SizeTypeId, RussianSize = x.RussianSize})
          .ToList(),
        Photos = dto.ProductPhotos.Select(x => UrlConverter.Convert(x.PhotoUrl, ConvertedType.Product)).ToList()
      };
      return result;
    }
  }

  public class SizeViewModel
  {
    public int SizeTypeId { get; set; }
    public string RussianSize { get; set; }
  }

  public class BrandViewModel
  {
    public int BrandId { get; set; }
    public string Name { get; set; }
  }
}