using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities.ProductGroup
{
  [Table("SizeType")]
  public class SizeTypeEntity
  {
    [Key]
    public int SizeTypeId { get; set; }

    public string Russian { get; set; }
    public string OtherCountrySize { get; set; }
    public string CountryType { get; set; }
  }
}