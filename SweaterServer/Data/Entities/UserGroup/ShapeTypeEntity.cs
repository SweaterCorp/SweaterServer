using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities.UserGroup
{
  [Table("ShapeType")]
  public class ShapeTypeEntity
  {
    [Key]
    public int ShapeTypeId { get; set; }
    public string RussianName { get; set; }
    public string EnglishName { get; set; }
  }
}
