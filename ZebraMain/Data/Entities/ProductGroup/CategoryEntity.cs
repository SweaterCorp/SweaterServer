using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities.ProductGroup
{
  [Table("Category")]
  public class CategoryEntity
  {
    [Key]
    public int CategoryId { get; set; }
    public string CategoryPhotoUrl { get; set; }
    public string RussianName { get; set; }
    public string EnglishName { get; set; }
  }
}
