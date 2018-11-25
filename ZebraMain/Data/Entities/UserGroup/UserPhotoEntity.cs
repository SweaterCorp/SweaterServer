using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZebraData.Entities.UserGroup
{
  [Table("UserPhoto")]
  public class UserPhotoEntity
  {
    public int UserPhotoId { get; set; }
    public int UserId { get; set; }
    public string PhotoUrl { get; set; }
  }
}
