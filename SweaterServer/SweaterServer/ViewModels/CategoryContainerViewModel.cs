using System.Collections.Generic;
using ProductDatabase.Entities;

namespace SweaterMain.ViewModels
{
  public class CategoryContainerViewModel<T>
  {
    public CategoryEntity Category { get; set; }
    public ListResponseViewModel<T> Data { get; set; }

    public CategoryContainerViewModel ()
    {
    }

    public CategoryContainerViewModel (CategoryEntity category, int count, List<T> list)
    {
      Category = category;
      Data = new ListResponseViewModel<T> {Count = count, List = list};
    }
  }
}