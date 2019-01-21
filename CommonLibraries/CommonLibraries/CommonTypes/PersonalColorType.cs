using System.Collections.Generic;
using CommonLibraries.Infrastructures;

namespace CommonLibraries.CommonTypes
{
  public class PersonalColorType : CustomEnum
  {
    public static PersonalColorType None { get; } = new PersonalColorType(0, "None");
    public static PersonalColorType Autumn { get; } = new PersonalColorType(1, "Autumn");
    public static PersonalColorType Spring { get; } = new PersonalColorType(2, "Spring");
    public static PersonalColorType Summer { get; } = new PersonalColorType(3, "Summer");
    public static PersonalColorType Winter { get; } = new PersonalColorType(4, "Winter");


    private PersonalColorType(int id, string name) : base(id, name)
    {
    }

    public static List<PersonalColorType> AsList()
    {
      return new List<PersonalColorType> {None, Winter, Spring, Summer, Autumn};
    }

    public static explicit operator PersonalColorType(int id)
    {
      return AsList().Find(x => x.Id == id);
    }

    public static explicit operator PersonalColorType(string name)
    {
      return AsList().Find(x => x.Name == name);
    }
  }
}