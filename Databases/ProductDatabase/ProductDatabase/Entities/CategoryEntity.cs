using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductDatabase.Entities
{
  [Table("Category")]
  public class CategoryEntity
  {
    [Key]
    public int CategoryId { get; set; }
    public int CategoryTypeId { get; set; }
    public int ClicksCount { get; set; }
    public string CategoryPhotoUrl { get; set; }
  }
}
