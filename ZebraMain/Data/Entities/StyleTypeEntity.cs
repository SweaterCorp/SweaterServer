using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities
{
  [Table("ColorType")]
  public class StyleTypeEntity : InernationalName
  {
    [Key]
    public int StyleTypeId { get; set; }
  }
}