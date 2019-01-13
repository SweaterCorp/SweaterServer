using System.IO;

namespace CommonLibraries.Resources
{
  public class ContentPath
  {
    private string BasePath { get; }
    public string Reources => Path.Combine(BasePath, "Resources");

    public string ColorsMatching => Path.Combine(Reources, "ColorsMatching.json");
    public string LamodaColors => Path.Combine(Reources, "LamodaColors.json");

    public ContentPath(string resourceContainigFolder = null)
    {
      BasePath = !string.IsNullOrEmpty(resourceContainigFolder) ? resourceContainigFolder : Directory.GetCurrentDirectory();
    }
  }
}