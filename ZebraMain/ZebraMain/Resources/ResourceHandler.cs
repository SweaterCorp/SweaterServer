using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZebraMain.Resources
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
