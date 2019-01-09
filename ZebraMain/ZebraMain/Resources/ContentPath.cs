using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ZebraMain.Resources
{
  public class Content
  {
    private string ContentPath { get; }
    public string Reources => Path.Combine(ContentPath, "Resources");

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