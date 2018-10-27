using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities
{
  [Table("ProductPhoto")]
  public class ProductPhotoEntity
  {
    [Key]
    public int ProductPhotoId { get; set; }

    public int ProductId { get; set; }
    public string PhotoUrl { get; set; }
  }
}