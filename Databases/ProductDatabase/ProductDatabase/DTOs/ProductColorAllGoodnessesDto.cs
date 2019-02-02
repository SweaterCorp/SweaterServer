using System.Collections.Generic;

namespace ProductDatabase.DTOs
{
  public class ProductWithColorGoodnessDto
  {
    public int ProductId { get; set; }
    public List<ProductColorGoodnessDto> ProductColorGoodnesses { get; set; }
  }

  public class ProductColorGoodnessDto
  {
    public int PersonalColorTypeId { get; set; }
    public int ColorId { get; set; }
    public float Goodness { get; set; }
  }
}