using System.Collections.Generic;

namespace ZebraData.DTOs
{
  public class ProductsFilterDto
  {
    public int CategoryId { get; set; }
    public decimal MinimalPrice { get; set; } = decimal.Zero;
    public decimal MaximalPrice { get; set; } = decimal.MaxValue;
    public List<int> BrandsIds { get; set; } = new List<int>();
    public List<int> ColorsIds { get; set; } = new List<int>();
    public List<int> SizesIds { get; set; } = new List<int>();
  }
}