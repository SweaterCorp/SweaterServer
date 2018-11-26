using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities.UserGroup
{
  [Table("HumanColorType")]
  public class HumanColorTypeEntity
  {
    [Key]
    public int HumanColorTypeId { get; set; }

    public string RussianName { get; set; }
    public string EnglishName { get; set; }
  }
}