using System.Collections.Generic;

namespace ZebraMain.ViewModels
{
  public class ListResponseViewModel<T>
  {
    public int Count { get; set; }

    public List<T> List { get; set; }
  }
}