using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities.ProductGroup
{
  [Table("Brand")]
  public class BrandEntity
  {
    [Key]
    public int BrandId { get; set; }

    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public string Site { get; set; }
  }
}