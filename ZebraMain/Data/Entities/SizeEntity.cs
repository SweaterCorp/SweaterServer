using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities
{
  [Table("Size")]
  public class SizeEntity
  {
    [Key]
    public int SizeId { get; set; }

    public string Russian { get; set; }
    public string Europa { get; set; }
    public string China { get; set; } // ReSharper disable InconsistentNaming
    public string USA { get; set; }
  }
}