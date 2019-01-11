using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace CommonLibraries.Resources
{
  public class ResourceHandler
  {
    public string ReadResourceFile(string path)
    {
      using (var sr = new StreamReader(path, Encoding.UTF8))
      {
        var str = sr.ReadToEnd();
        return str;
      }
    }

    public T ReadeResourceFile<T>(string path)
    {
      using (var sr = new StreamReader(path, Encoding.UTF8))
      {
        var str = sr.ReadToEnd();
        var result = JsonConvert.DeserializeObject<T>(str);
        return result;
      }
    }
  }
}
