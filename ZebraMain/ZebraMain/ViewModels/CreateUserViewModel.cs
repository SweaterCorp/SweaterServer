using System;
using System.ComponentModel.DataAnnotations;

namespace ZebraMain.ViewModels
{
  public class UserViewModel
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? HumanColorTypeId { get; set; }
    public int? ShapeTypeId { get; set; }
    public int? SexTypeId { get; set; }
  }
}