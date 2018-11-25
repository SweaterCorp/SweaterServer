using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZebraData.Entities.UserGroup
{
  [Table("HumanColorType")]
  public class HumanColorTypeEntity
  {
    public int HumanColorTypeId { get; set; }
    public string RussianName { get; set; }
    public string EnglishName { get; set; }
  }
}
