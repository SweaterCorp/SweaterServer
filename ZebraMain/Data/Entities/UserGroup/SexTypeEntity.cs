using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZebraData.Entities.UserGroup
{
  [Table("SexType")]
  public class SexTypeEntity
  {
    public int SexTypeId { get; set; }
    public string RussianName { get; set; }
    public string EnglishName { get; set; }
  }
}
