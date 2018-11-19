using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities.ProductGroup
{
  [Table("ColorType")]
  public class ColorTypeEntity 
  {
    [Key]
    public int ColorTypeId { get; set; }
 
    public string Hex { get; set; }
    public string RussianName { get; set; }
    public string EnglishName { get; set; }
  }
}