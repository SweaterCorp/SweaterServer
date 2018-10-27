using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities
{
  [Table("ProductSize")]
  public class ProductSizeEntity
  {
    public int ProductId { get; set; }
    public int SizeId { get; set; }
    public int ColorId { get; set; }
    public int Count { get; set; }
    public DateTime UpdatedDate { get; set; }
  }
}