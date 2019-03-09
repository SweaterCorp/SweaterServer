using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities.ProductGroup
{
  [Table("PrintType")]
  public class PrintTypeEntity
  {
    [Key]
    public int PrintTypeId { get; set; }

    public string RussianName { get; set; }
    public string EnglishName { get; set; }
  }
}