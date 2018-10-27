using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZebraData.Entities
{
  [Table("Brand")]
  public class CategoryEntity : InernationalName
  {
    [Key]
    public int CategoryId { get; set; }
    public string CategoryPhoroUrl { get; set; }
  }
}
