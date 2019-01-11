using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities.ProductGroup
{
  [Table("ProductSizeType")]
  public class ProductSizeTypeEntity
  {
    [Key]
    public int ProductId { get; set; }
    public int SizeTypeId { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime UpdatedDate { get; set; }
  }
}