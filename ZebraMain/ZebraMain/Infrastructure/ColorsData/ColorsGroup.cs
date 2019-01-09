using System.Collections.Generic;
using CommonLibraries.Infrastructures;

namespace ZebraMain.Infrastructure.ColorsData
{
  public class ColorsGroup
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<BaseColor> BaseColors { get; set; }
    public List<ServerColor> GoodColors { get; set; }
    public List<ServerColor> BadColors { get; set; }
  }
}
