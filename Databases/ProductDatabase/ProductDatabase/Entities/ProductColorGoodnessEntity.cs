using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductDatabase.Entities
{
  [Table("ProductColorGoodness")]
  public class ProductColorGoodnessEntity
  {
    [Key]
    public int ProductColorGoodnessId { get; set; }

    public int ProductId { get; set; }
    public int PersonalColorTypeId { get; set; }
    public float Goodness { get; set; }
  }
}