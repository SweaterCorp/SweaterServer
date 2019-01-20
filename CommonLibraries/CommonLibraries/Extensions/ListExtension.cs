using System.Collections.Generic;

namespace CommonLibraries.Extensions
{
  public static class ListExtension
  {
    public static T GetValueOrDefault<T>(this List<T> list, int index)
    {
      return GetValueOrDefault(list, index, default(T));
    }

    public static T GetValueOrDefault<T>(this List<T> list, int index, T @default)
    {
      return list.Count >= index + 1 ? list[index] : @default;
    }
  }
}