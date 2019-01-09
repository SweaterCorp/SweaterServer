namespace CommonLibraries.Infrastructures
{
  public class CustomEnum
  {
    public int Id { get; set; }
    public string Name { get; set; }

    protected CustomEnum()
    {
    }

    public CustomEnum(int id, string name)
    {
      Id = id;
      Name = name;
    }
  }
}