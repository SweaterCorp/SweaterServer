using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities.UserGroup
{
  [Table("User")]
  public class UserEntity
  {
    [Key]
    public int UserId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int HumanColorType { get; set; }
    public int ShapeType { get; set; }
    public int SexType { get; set; }
    public DateTime BirthDate { get; set; }
  }
}