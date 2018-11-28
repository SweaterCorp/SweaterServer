using System.Collections.Generic;
using System.Linq;
using CommonLibraries.Exceptions.ApiExceptions;
using ZebraData.Entities.UserGroup;

namespace ZebraData.Repositories
{
  public class UserRepository
  {
    private readonly ZebraMainContext _db;

    public UserRepository(ZebraMainContext db)
    {
      _db = db;
    }

    public UserEntity CreateUser(UserEntity user)
    {
      _db.UserEntities.Add(user);
      _db.SaveChanges();
      return user;
    }

    public UserEntity UpdateUser(int userId, UserEntity user)
    {
      var userEntity = _db.UserEntities.FirstOrDefault(x => x.UserId == userId) ??
                       throw new NotFoundException($"There is no user with id: {userId}.");

      userEntity.FirstName = user.FirstName;
      user.LastName = user.LastName;
      user.SexType = user.SexType;
      user.ShapeType = user.ShapeType;
      user.HumanColorType = user.HumanColorType;

      _db.SaveChanges();

      return user;
    }

    public UserEntity GetUser(int userId)
    {
      var user = _db.UserEntities.FirstOrDefault(x => x.UserId == userId) ?? new UserEntity();
      return user;
    }

    public UserPhotoEntity InsertUserPhoto(int userId, string url)
    {
      var userEntity = _db.UserEntities.FirstOrDefault(x => x.UserId == userId) ??
                       throw new NotFoundException($"There is no user with id: {userId}.");
      var userPhoto = new UserPhotoEntity {PhotoUrl = url, UserId = userId};
      _db.UserPhotoEntities.Add(userPhoto);
      _db.SaveChanges();

      return userPhoto;
    }

    public (UserEntity user, List<UserPhotoEntity> photos) GetUserWithPhotos(int userId)
    {
      var user = GetUser(userId);
      var photos = _db.UserPhotoEntities.Where(x => x.UserId == userId).ToList();

      return (user, photos);
    }
  }
}