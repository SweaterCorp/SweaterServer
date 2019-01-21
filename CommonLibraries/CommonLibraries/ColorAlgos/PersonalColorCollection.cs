using System.Collections.Generic;
using CommonLibraries.CommonTypes;

namespace CommonLibraries.ColorAlgos
{
  public class PersonalColorCollection
  {
    public PersonalColor Autumn { get; set; }
    public PersonalColor Spring { get; set; }
    public PersonalColor Summer { get; set; }
    public PersonalColor Winter { get; set; }

    public PersonalColorCollection()
    {
      Autumn = SetAutumn();
      Spring = SetSpring();
      Summer = SetSummer();
      Winter = SetWinter();
    }

    private PersonalColor SetAutumn()
    {
      var result = new PersonalColor
      {
        PersonalColorType = PersonalColorType.Autumn,
        Description = "Осень",
        EyeColors = new List<string> {"996c2b", "48281d", "6f4d32", "625737", "626936"},
        HairColors = new List<string> {"a47849", "884a21", "8c4124", "7a462e", "4d2919"},
        SkinTones = new List<string> {"e79e7e", "f6c4a3", "f6c4a3", "d27c65"}
      };
      return result;
    }

    private PersonalColor SetSpring()
    {
      var result = new PersonalColor
      {
        PersonalColorType = PersonalColorType.Spring,
        Description = "Весна",
        EyeColors = new List<string> {"4f7e6a", "8c9657", "777d63", "b3945d", "a77133"},
        HairColors = new List<string> {"e8c899", "cc8c60", "b27a41", "d28f4b", "864d32"},
        SkinTones = new List<string> {"f9b993", "fbc8b5", "fab191", "da8f67"}
      };
      return result;
    }

    private PersonalColor SetSummer()
    {
      var result = new PersonalColor
      {
        PersonalColorType = PersonalColorType.Summer,
        Description = "Лето",
        EyeColors = new List<string> {"7b8989", "4b6468", "607056", "6d7369", "6d7369"},
        HairColors = new List<string> {"311d1c", "7c5a3e", "9b8879"},
        SkinTones = new List<string> {"c59f8a", "dfb09e", "ffd0bc", "ac8c77"}
      };
      return result;
    }

    private PersonalColor SetWinter()
    {
      var result = new PersonalColor
      {
        PersonalColorType = PersonalColorType.Winter,
        Description = "Зима",
        EyeColors = new List<string> {"563820", "4b3a26", "0e0c0d", "28323e", "3c404c"},
        HairColors = new List<string> {"000000", "40332b", "483d3b"},
        SkinTones = new List<string> {"e4b798", "d0a497", "f9dfd0", "795147"}
      };
      return result;
    }
  }

  public class PersonalColor
  {
    public PersonalColorType PersonalColorType { get; set; }
    public string Description { get; set; }
    public List<string> EyeColors { get; set; } = new List<string>();
    public List<string> HairColors { get; set; } = new List<string>();
    public List<string> SkinTones { get; set; } = new List<string>();
  }
}