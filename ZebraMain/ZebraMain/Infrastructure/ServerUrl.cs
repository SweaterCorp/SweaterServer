using System.IO;

namespace ZebraMain.Infrastructure
{
  public class ServerUrl
  {
    private string[] Fragments { get; }

    public ServerUrl(string url)
    {
      Fragments = url.Contains('/') ? url.Split('/') : url.Split('\\');
    }

    public ServerUrl(string[] fragments)
    {
      Fragments = fragments;
    }

    public string ToPcUrl()
    {
      return Path.Combine(Fragments);
    }

    public string ToWebUrl()
    {
      return string.Join('/', Fragments);
    }

    public static bool IsNullOrEmpty(ServerUrl serverUrl)
    {
      return serverUrl == null || serverUrl.Fragments == null || serverUrl.Fragments.Length < 0;
    }
  }
}