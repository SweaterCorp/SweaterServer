using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities.ProductGroup
{
  [Table("ProductSize")]
  public class ProductColorTypeEntity
  {
    public int ProductColorTypeId { get; set; }
    public int ColorTypeId { get; set; }
    public DateTime UpdatedDate { get; set; }
  }
}