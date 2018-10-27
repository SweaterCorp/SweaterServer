using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities
{
  [Table("ColorType")]
  public class ColorTypeEntity : InernationalName
  {
    [Key]
    public int ColorTypeId { get; set; }

    public string Hex { get; set; }
  }
}