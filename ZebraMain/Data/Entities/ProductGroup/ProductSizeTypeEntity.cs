using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities.ProductGroup
{
  [Table("ProductSize")]
  public class ProductSizeTypeEntity
  {
    public int ProductId { get; set; }
    public int SizeTypeId { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime UpdatedDate { get; set; }
  }
}