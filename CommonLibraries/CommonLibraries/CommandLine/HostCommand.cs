namespace CommonLibraries.CommandLine
{
  public class HostCommand : Command
  {
    public string Port { get; set; }
    public string Swagger { get; set; }
    public string CurrentDirectory { get; set; }
  }
}