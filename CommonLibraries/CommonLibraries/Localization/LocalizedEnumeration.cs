using System.Collections.Generic;
using System.Linq;
using CommonLibraries.Infrastructures;

namespace CommonLibraries.Localization
{
  public class LocalizedEnumeration : Enumeration
  {
    protected List<TranslationName> TranslationNames = new List<TranslationName>();

    protected string this[LaguageType laguageType] => TranslationNames.FirstOrDefault(x => x.LaguageType == laguageType)
                                                        ?.Name ?? TranslationNames[0].Name;

    protected static void SetTranslations<T>(List<(int typeId, List<TranslationName> localizationNames)> translations,
      IEnumerable<T> list) where T : LocalizedEnumeration
    {
      foreach (var (typeId, localizationNames) in translations)
        FromValue(typeId, list).TranslationNames = localizationNames;
    }
  }

  public class SexTypeLocalizatonNames
  {
    public static List<(int typeId, List<TranslationName>)> GetLocalizatonNames()
    {
      var result = new List<(int typeId, List<TranslationName> localizationNames)>
      {
        (0, new List<TranslationName>
        {
          new TranslationName(LaguageType.Default, "None"),
          new TranslationName(LaguageType.English, "None"),
          new TranslationName(LaguageType.Russian, "Неизвестно")
        }),

        (1, new List<TranslationName>
        {
          new TranslationName(LaguageType.Default, "Male"),
          new TranslationName(LaguageType.English, "Male"),
          new TranslationName(LaguageType.Russian, "Мужской")
        }),

        (2, new List<TranslationName>
        {
          new TranslationName(LaguageType.Default, "Female"),
          new TranslationName(LaguageType.English, "Female"),
          new TranslationName(LaguageType.Russian, "Женщина")
        })
      };
      return result;
    }
  }
}