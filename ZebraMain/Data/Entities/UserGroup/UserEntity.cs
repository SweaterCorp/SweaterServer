using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CommonLibraries.CommonTypes;

namespace ZebraData.Entities.UserGroup
{
  [Table("User")]
  public class UserEntity
  {
    [Key]
    public int UserId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int HumanColorTypeId { get; set; }
    public int ShapeTypeId { get; set; }
    public int SexTypeId { get; set; }
    public DateTime BirthDate { get; set; }
  }
}
