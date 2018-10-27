namespace CommonLibraries.CommandLine
{
  public class OrCommandLinePattern : CommandLinePattern
  {
    private readonly CommandLinePattern _left;
    private readonly CommandLinePattern _right;

    public OrCommandLinePattern(CommandLinePattern left, CommandLinePattern right)
    {
      _left = left;
      _right = right;
    }

    public override bool TryParse(string[] args, out Command result)
    {
      return _left.TryParse(args, out result) || _right.TryParse(args, out result);
    }
  }
}