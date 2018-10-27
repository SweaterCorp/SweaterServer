using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities
{
  [Table("Color")]
  public class ColorEntity : InernationalName
  {
    [Key]
    public int ColorId { get; set; }

    public string Hex { get; set; }
  }
}