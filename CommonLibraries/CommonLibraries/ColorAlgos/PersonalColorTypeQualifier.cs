using System.Collections.Generic;
using System.Linq;
using CommonLibraries.CommonTypes;
using CommonLibraries.Extensions;

namespace CommonLibraries.ColorAlgos
{
  public class PersonalColorTypeQualifier
  {
    public PersonalColorCollection Collection { get; set; } = new PersonalColorCollection();

    public PersonalColorType GetPersonalColorType(string eyeColor, string hairColor, string skinTone)
    {
      var ranking = new List<double>
      {
        BelongTo(Collection.Autumn),
        BelongTo(Collection.Spring),
        BelongTo(Collection.Summer),
        BelongTo(Collection.Winter)
      };

      var max = ranking.Max();
      if (ranking[0].EqualsStrict(max)) return PersonalColorType.Autumn;
      if (ranking[1].EqualsStrict(max)) return PersonalColorType.Spring;
      if (ranking[2].EqualsStrict(max)) return PersonalColorType.Summer;
      return PersonalColorType.Winter;

      double BelongTo(PersonalColor personalColor)
      {
        var result = 0.0;
        if (personalColor.EyeColors.Contains(eyeColor)) result += 1.5;
        if (personalColor.HairColors.Contains(hairColor)) result++;
        if (personalColor.SkinTones.Contains(skinTone)) result++;
        return result;
      }
    }
  }
}