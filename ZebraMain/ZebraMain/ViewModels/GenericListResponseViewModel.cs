using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZebraMain.ViewModels
{
  public class GenericListResponseViewModel<T>
  {
    public int Count { get; set; }

    public List<T> List { get; set; }
  }
}
