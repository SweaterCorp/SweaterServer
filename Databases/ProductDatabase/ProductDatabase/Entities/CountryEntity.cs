using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductDatabase.Entities
{
  [Table("Country")]
  public class CountryEntity
  {
    [Key]
    public int CountryId { get; set; }

    public string FlagUrl { get; set; }
    public string EnglishName { get; set; }
    public string RussianName { get; set; }
  }
}