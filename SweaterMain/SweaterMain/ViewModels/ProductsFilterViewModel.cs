using System.Collections.Generic;

namespace SweaterMain.ViewModels
{
  public class ProductsFilterViewModel
  {
    public decimal MinimalPrice { get; set; } = decimal.MinValue;
    public decimal MaximalPrice { get; set; } = decimal.MaxValue;
    public List<int> BrandsIds { get; set; } = new List<int>();
    public List<int> ColorsIds { get; set; } = new List<int>();
    public List<int> SizesIds { get; set; } = new List<int>();
    public PageParams PageParams { get; set; } = new PageParams();
  }
}