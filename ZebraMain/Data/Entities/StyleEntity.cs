using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities
{
  [Table("Style")]
  public class StyleEntity : InernationalName
  {
    [Key]
    public int StyleId { get; set; }
  }
}