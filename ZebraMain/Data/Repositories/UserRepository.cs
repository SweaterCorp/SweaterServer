using System.Linq;
using CommonLibraries.Exceptions.ApiExceptions;
using ZebraData.DTOs;
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

    public enum UpdatedType
    {
      Shape = 0,
      HumanColor = 1,
      Sex
    }

    public UserDto GetUser(int userId)
    {
      var user = _db.UserEntities.FirstOrDefault(x => x.UserId == userId) ??
                 throw new NotFoundException($"There is no user with id: {userId}.");

      var sexType = _db.SexTypeEntities.FirstOrDefault(x => x.SexTypeId == user.SexTypeId);
      var humanColorType = _db.HumanColorTypeEntities.FirstOrDefault(x => x.HumanColorTypeId == user.HumanColorTypeId);
      var shapeType = _db.ShapeTypeEntities.FirstOrDefault(x => x.ShapeTypeId == user.SexTypeId);

      var result = new UserDto
      {
        UserId = user.UserId,
        BirthDate = user.BirthDate,
        FirstName = user.FirstName,
        HumanColorType = humanColorType,
        LastName = user.LastName,
        SexType = sexType,
        ShapeType = shapeType
      };

      return result;
    }

    public UserDto CreateUser(UserEntity user)
    {
      var sexType = _db.SexTypeEntities.FirstOrDefault(x => x.SexTypeId == user.SexTypeId) ??
                    throw new NotFoundException($"There is no sex type with id: {user.SexTypeId}.");
      var shapeType = _db.ShapeTypeEntities.FirstOrDefault(x => x.ShapeTypeId == user.ShapeTypeId) ??
                      throw new NotFoundException($"There is no shape type with id: {user.ShapeTypeId}.");
      var humanColorType =
        _db.HumanColorTypeEntities.FirstOrDefault(x => x.HumanColorTypeId == user.HumanColorTypeId) ??
        throw new NotFoundException($"There is no human color type with id: {user.HumanColorTypeId}.");

      _db.UserEntities.Add(user);

      _db.SaveChanges();

      var result = new UserDto
      {
        UserId = user.UserId,
        BirthDate = user.BirthDate,
        FirstName = user.FirstName,
        HumanColorType = humanColorType,
        LastName = user.LastName,
        SexType = sexType,
        ShapeType = shapeType
      };

      return result;
    }

    public UserDto UpdateUserType(int userId, int updatedTypeId, UpdatedType updatedType)
    {
      var user = _db.UserEntities.FirstOrDefault(x => x.UserId == userId) ??
                 throw new NotFoundException($"There is no user with id: {userId}.");

      var sexType = _db.SexTypeEntities.FirstOrDefault(x => x.SexTypeId == user.SexTypeId);
      var humanColorType = _db.HumanColorTypeEntities.FirstOrDefault(x => x.HumanColorTypeId == user.HumanColorTypeId);
      var shapeType = _db.ShapeTypeEntities.FirstOrDefault(x => x.ShapeTypeId == user.SexTypeId);

      switch (updatedType)
      {
        case UpdatedType.Shape:
          shapeType = _db.ShapeTypeEntities.FirstOrDefault(x => x.ShapeTypeId == updatedTypeId) ??
                      throw new NotFoundException($"There is no shape type with id: {user.SexTypeId}.");
          user.ShapeTypeId = updatedTypeId;
          break;
        case UpdatedType.HumanColor:
          humanColorType = _db.HumanColorTypeEntities.FirstOrDefault(x => x.HumanColorTypeId == updatedTypeId) ??
                           throw new NotFoundException($"There is no shape type with id: {user.SexTypeId}.");
          user.HumanColorTypeId = updatedTypeId;
          break;
        case UpdatedType.Sex:
          sexType = _db.SexTypeEntities.FirstOrDefault(x => x.SexTypeId == updatedTypeId) ??
                    throw new NotFoundException($"There is no shape type with id: {user.SexTypeId}.");
          user.SexTypeId = updatedTypeId;
          break;
      }

      _db.SaveChanges();

      var result = new UserDto
      {
        UserId = user.UserId,
        BirthDate = user.BirthDate,
        FirstName = user.FirstName,
        HumanColorType = humanColorType,
        LastName = user.LastName,
        SexType = sexType,
        ShapeType = shapeType
      };

      return result;
    }
  }
}