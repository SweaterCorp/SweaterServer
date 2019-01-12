using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductDatabase.Entities
{
  [Table("SizeType")]
  public class SizeTypeEntity
  {
    [Key]
    public int SizeTypeId { get; set; }

    public string RussianSize { get; set; }
    public string OtherSize { get; set; }
    public string CountryCode { get; set; }
  }
}