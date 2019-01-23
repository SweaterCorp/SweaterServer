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
      var newUser = new UserEntity {PersonalColorTypeId = personalColorType.Id, PhoneIMEI = phoneImei};
      Db.UserEntities.Add(newUser);
      await Db.SaveChangesAsync();
      return newUser;
    }

    public async Task<UserEntity> GetUser(string phoneImei)
    {
      return await Db.UserEntities.FirstOrDefaultAsync(x => x.PhoneIMEI == phoneImei);
    }

    public async Task<UserEntity> GetUser(int userId)
    {
      return await Db.UserEntities.FindAsync(userId);
    }
  }
}