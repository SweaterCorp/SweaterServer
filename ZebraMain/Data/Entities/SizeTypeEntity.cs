using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZebraData.Entities
{
  [Table("SizeType")]
  public class SizeTypeEntity
  {
    public int SizeTypeId { get; set; }
    public string Russian { get; set; }
    public string Europa { get; set; }
    public string China { get; set; }

    // ReSharper disable InconsistentNaming
    public string USA { get; set; }
  }
}
