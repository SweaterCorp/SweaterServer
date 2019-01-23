using System.Collections.Generic;

namespace SweaterMain.ViewModels
{
  public class ProductsFilterWithColorViewModel
  {
    public decimal MinimalPrice { get; set; } = decimal.MinValue;
    public decimal MaximalPrice { get; set; } = decimal.MaxValue;
    public List<int> BrandsIds { get; set; } = new List<int>();
    public List<int> SizesIds { get; set; } = new List<int>();
    public int PersonalColorTypeId { get; set; }
    public int ColorCategoryId { get; set; }
    public PageParams PageParams { get; set; } = new PageParams();
  }
}