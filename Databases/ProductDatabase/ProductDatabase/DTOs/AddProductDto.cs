using System.Collections.Generic;

namespace ProductDatabase.DTOs
{
  public class AddProductDto
  {
    public string VendorCode { get; set; }
    public string BrandName { get; set; }
    public int CategoryTypeId { get; set; }
    public int PrintTypeId { get; set; }
    public int ColorId { get; set; }
    public decimal Price { get; set; }
    public string Country { get; set; }
    public string Link { get; set; }
    public List<SizeDto> Sizes { get; set; }
    public List<string> Photos { get; set; }
  }

  public class SizeDto
  {
    public bool IsAvailable { get; set; }
    public string RussianSize { get; set; }
    public string OtherSize { get; set; }
    public string CountryCode { get; set; }
  }
}