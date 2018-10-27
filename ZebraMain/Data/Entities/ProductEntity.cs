using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZebraData.Entities
{
  [Table("Product")]
  public class ProductEntity
  {
    [Key]
    public int ProductId { get; set; }
  }
}
