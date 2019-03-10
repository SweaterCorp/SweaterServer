namespace SweaterServer.ViewModels
{
  public class TypeViewModel
  {
    public TypeViewModel() { }

    public TypeViewModel(int id, string key, string displayedName)
    {
      Id = id;
      Key = key;
      DisplayedName = displayedName;
    }

    public int Id { get; set; }
    public string Key { get; set; }
    public string DisplayedName { get; set; }
  }
}
