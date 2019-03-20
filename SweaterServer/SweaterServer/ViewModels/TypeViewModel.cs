namespace SweaterServer.ViewModels
{
  public class TypeViewModel
  {
    public int Id { get; set; }
    public string DisplayedName { get; set; }

    public TypeViewModel()
    {
    }

    public TypeViewModel(int id, string displayedName)
    {
      Id = id;
      DisplayedName = displayedName;
    }
  }
}