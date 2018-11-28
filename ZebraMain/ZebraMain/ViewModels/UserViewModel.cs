using CommonLibraries.Localization;
using ZebraData.Entities.UserGroup;

namespace ZebraMain.ViewModels
{
  public class UserViewModel
  {
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public TypeViewModel HumanColorType { get; set; }
    public TypeViewModel ShapeType { get; set; }
    public TypeViewModel SexType { get; set; }

    public static UserViewModel GetDefault()
    {
      return new UserViewModel();
    }

    public static UserViewModel CreateFromUserEntity(UserEntity user)
    {
      var humanColorType = CommonLibraries.CommonTypes.HumanColorType.FromValue(user.HumanColorType);
      var shapeType = CommonLibraries.CommonTypes.ShapeType.FromValue(user.ShapeType);
      var sexType = CommonLibraries.CommonTypes.SexType.FromValue(user.SexType);

      return new UserViewModel
      {
        UserId = user.UserId,
        FirstName = user.FirstName,
        LastName = user.LastName,
        HumanColorType = new TypeViewModel(humanColorType.Id, humanColorType.Key, humanColorType[LaguageType.Russian]),
        ShapeType = new TypeViewModel(shapeType.Id, shapeType.Key, shapeType[LaguageType.Russian]),
        SexType = new TypeViewModel(sexType.Id, sexType.Key, sexType[LaguageType.Russian])
      };
    }
  }
}