using System.Collections.Generic;
using CommonLibraries.Infrastructures;

namespace CommonLibraries.CommonTypes
{
  public class RoleType : Enumeration
  {
    public static RoleType Guest { get; } = new RoleType(0, "Guest");
    public static RoleType User { get; } = new RoleType(1, "User");
    public static RoleType Moderator { get; } = new RoleType(2, "Moderator");
    public static RoleType Administrator { get; } = new RoleType(3, "Administrator");

    private RoleType(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<RoleType> List()
    {
      return new List<RoleType> {Guest, User, Moderator, Administrator};
    }

    public static RoleType FromString(string name)
    {
      return FromString(name, List());
    }

    public static RoleType FromValue(int id)
    {
      return FromValue(id, List());
    }
  }
}