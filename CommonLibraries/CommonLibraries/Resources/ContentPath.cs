using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CommonLibraries.Resources
{
  public class Content
  {
    private string ContentPath { get; }
    public string Reources => Path.Combine(ContentPath, "Resources");

    public string ColorsMatching => Path.Combine(ContentPath, "ColorsMatching.json");
    public string LamodaColors => Path.Combine(ContentPath, "LamodaColors.json");

    public Content(string basePath)
    {
      ContentPath = basePath;
    }

    public Content(IHostingEnvironment env)
    {
      ContentPath = env.ContentRootPath;
    }

    public string GetReourcePath(string resourceName)
    {
      return Path.Combine(ContentPath, "Resources", resourceName);
    }
  }
}