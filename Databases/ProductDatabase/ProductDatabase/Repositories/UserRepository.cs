using System.Threading.Tasks;
using CommonLibraries.CommonTypes;
using Microsoft.EntityFrameworkCore;
using ProductDatabase.Entities;

namespace ProductDatabase.Repositories
{
  public class UserRepository
  {
    private ProductContext Db { get; }

    public UserRepository(ProductContext db)
    {
      Db = db;
    }

    public async Task<UserEntity> CreateUser(string phoneImei, PersonalColorType personalColorType)
    {
      var newUser = new UserEntity {PersonalColorTypeId = (int)personalColorType, PhoneIMEI = phoneImei};
      Db.UserEntities.Add(newUser);
      await Db.SaveChangesAsync();
      return newUser;
    }

    public async Task<UserEntity> UpdateUser(UserEntity userEntity)
    {
      Db.UserEntities.Update(userEntity);
      await Db.SaveChangesAsync();
      return userEntity;
    }


    public async Task<UserEntity> GetUser(string phoneImei)
    {
      return await Db.UserEntities.FirstOrDefaultAsync(x => x.PhoneIMEI == phoneImei);
    }
  }
}