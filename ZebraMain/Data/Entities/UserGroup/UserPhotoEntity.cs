using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities.UserGroup
{
  [Table("UserPhoto")]
  public class UserPhotoEntity
  {
    [Key]
    public int UserPhotoId { get; set; }
    public int UserId { get; set; }
    public string PhotoUrl { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
  }
}