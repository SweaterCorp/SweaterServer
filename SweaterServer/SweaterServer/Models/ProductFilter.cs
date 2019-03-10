using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SweaterServer.ViewModels;

namespace SweaterServer.Models
{
  public class ProductFilter
  {
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public int PersonalColorTypeId { get; set; }
    public decimal MinimalPrice { get; set; } = decimal.MinValue;
    public decimal MaximalPrice { get; set; } = decimal.MaxValue;
    public List<int> BrandsIds { get; set; } = new List<int>();
    public List<int> SizesIds { get; set; } = new List<int>();
    public PageParams PageParams { get; set; } = new PageParams();
  }
}