using System.Collections.Generic;
using CommonLibraries.Infrastructures;

namespace CommonLibraries.Old
{
  public class NotRoleType : NotEnumeration
  {
    public static NotRoleType Guest { get; } = new NotRoleType(0, "Guest");
    public static NotRoleType User { get; } = new NotRoleType(1, "User");
    public static NotRoleType Moderator { get; } = new NotRoleType(2, "Moderator");
    public static NotRoleType Administrator { get; } = new NotRoleType(3, "Administrator");

    private NotRoleType(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<NotRoleType> List()
    {
      return new List<NotRoleType> {Guest, User, Moderator, Administrator};
    }

    public static NotRoleType FromString(string name)
    {
      return FromString(name, List());
    }

    public static NotRoleType FromValue(int id)
    {
      return FromValue(id, List());
    }
  }
}