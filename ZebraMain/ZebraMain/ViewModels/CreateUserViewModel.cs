using System;
using System.ComponentModel.DataAnnotations;

namespace ZebraMain.ViewModels
{
  public class CreateUserViewModel
  {
    [Required]
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int HumanColorTypeId { get; set; }
    public int ShapeTypeId { get; set; }
    public int SexTypeId { get; set; }
    public DateTime BirthDate { get; set; }
    public int CityId { get; set; }
  }
}