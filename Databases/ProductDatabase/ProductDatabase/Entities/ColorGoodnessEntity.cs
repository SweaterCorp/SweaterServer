using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductDatabase.Entities
{
  [Table("ColorGoodness")]
  public class ColorGoodnessEntity
  {
    [Key]
    public int ColorGoodnessId { get; set; }

    public int ColorId { get; set; }
    public int PersonalColorTypeId { get; set; }
    public float Goodness { get; set; }
  }
}