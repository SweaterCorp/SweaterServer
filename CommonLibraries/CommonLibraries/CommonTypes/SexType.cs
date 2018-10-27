using System;
using System.Collections.Generic;
using System.Text;
using CommonLibraries.Infrastructures;

namespace CommonLibraries.CommonTypes
{
  public class SexType : Enumeration
  {
    public static SexType Both { get; } = new SexType(0, "Both");
    public static SexType Male { get; } = new SexType(1, "Male");
    public static SexType Female { get; } = new SexType(2, "Female");

    private SexType(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<SexType> List()
    {
      return new List<SexType> { Both, Male, Female };
    }

    public static SexType FromString(string name)
    {
      return FromString(name, List());
    }

    public static SexType FromValue(int id)
    {
      return FromValue(id, List());
    }
  }
}
