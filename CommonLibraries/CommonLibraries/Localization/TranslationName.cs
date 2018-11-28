namespace CommonLibraries.Localization
{
  public class TranslationName
  {
    public LaguageType LaguageType { get; set; }
    public string Name { get; set; }

    public TranslationName()
    {
    }

    public TranslationName(LaguageType laguage, string name)
    {
      LaguageType = laguage;
      Name = name;
    }
  }

  public enum LaguageType
  {
    Default = 0,
    Russian = 1,
    English = 2
  }
}