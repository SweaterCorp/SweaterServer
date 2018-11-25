using System;
using ZebraData.Entities.UserGroup;

namespace ZebraData.DTOs
{
  public class UserDto
  {
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public HumanColorTypeEntity HumanColorType { get; set; }
    public ShapeTypeEntity ShapeType { get; set; }
    public SexTypeEntity SexType { get; set; }
    public DateTime BirthDate { get; set; }
  }
}