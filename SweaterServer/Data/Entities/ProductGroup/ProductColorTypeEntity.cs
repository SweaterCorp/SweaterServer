using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities.ProductGroup
{
  [Table("ProductColorType")]
  public class ProductColorTypeEntity
  {
    [Key]
    public int ProductId { get; set; }
    public int ColorTypeId { get; set; }
    public DateTime UpdatedDate { get; set; }
  }
}