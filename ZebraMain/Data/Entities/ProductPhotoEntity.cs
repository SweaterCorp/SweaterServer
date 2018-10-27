using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZebraData.Entities
{


    [Table("ProductPhoto")]
    public class ProductPhotoEntity
  {
    [Key]
    public int ProductPhotoId { get; set; }
    public int ProductId { get; set; }
    public string PhotoUrl { get; set; }
  }
}
