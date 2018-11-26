using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities.UserGroup
{
  [Table("SexType")]
  public class SexTypeEntity
  {
    [Key]
    public int SexTypeId { get; set; }

    public string RussianName { get; set; }
    public string EnglishName { get; set; }
  }
}