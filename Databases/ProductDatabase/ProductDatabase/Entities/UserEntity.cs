using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductDatabase.Entities
{
  [Table("User")]
  public class UserEntity
  {
    [Key]
    public int UserId { get; set; } // ReSharper disable InconsistentNaming

    public string PhoneIMEI { get; set; }
    public int PersonalColorTypeId { get; set; }
  }
}