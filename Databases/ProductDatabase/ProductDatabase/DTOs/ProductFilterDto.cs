using System.Collections.Generic;
using CommonLibraries.CommonTypes;

namespace ProductDatabase.DTOs
{
  public class ProductFilterDto
  {
    public int CategoryId { get; set; }
    public decimal MinimalPrice { get; set; } = decimal.Zero;
    public decimal MaximalPrice { get; set; } = decimal.MaxValue;
    public List<int> BrandIds { get; set; } = new List<int>();
    public PersonalColorType PersonalColorType { get; set; }
    public List<int> SizeIds { get; set; } = new List<int>();
  }
}