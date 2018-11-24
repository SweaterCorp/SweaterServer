using System.Collections.Generic;

namespace ZebraMain.ViewModels
{
  public class ProductsFilterViewModel
  {
    public decimal MinimalPrice { get; set; }
    public decimal MaximalPrice { get; set; }
    public List<int> BrandsIds { get; set; }
    public List<int> ColorsIds { get; set; }
    public List<int> SizesIds { get; set; }
    public PageParams PageParams { get; set; }
  }
}