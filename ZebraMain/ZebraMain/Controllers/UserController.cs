using System.Net;
using CommonLibraries.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZebraData.Entities.UserGroup;
using ZebraData.Repositories;
using ZebraMain.ViewModels;

namespace ZebraMain.Controllers
{
  [Produces("application/json")]
  [EnableCors("AllowAllOrigin")]
  [Route("api/users")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private UserRepository Db { get; }
    private IHostingEnvironment AppEnvironment { get; }
    private ILogger<UserController> Logger { get; }

    public UserController(ILogger<UserController> logger, IHostingEnvironment appEnvironment, UserRepository db)
    {
      Logger = logger;
      Db = db;
      AppEnvironment = appEnvironment;
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserViewModel viewModel)
    {
      Logger.LogInformation($"{nameof(UserController)}.{nameof(CreateUser)}.Start");

      if (!IsValidUserViewModel(viewModel))
      {
        return new BadResponseResult(ModelState);
      }

      var user = Db.CreateUser(new UserEntity
      {
        FirstName = viewModel.FirstName,
        HumanColorTypeId = viewModel.HumanColorTypeId.GetValueOrDefault(),
        LastName = viewModel.LastName,
        SexTypeId = viewModel.SexTypeId.GetValueOrDefault(),
        ShapeTypeId = viewModel.ShapeTypeId.GetValueOrDefault(),
      });
      var result = new ResponseResult(HttpStatusCode.Created, $"User was created.", new {User = user});

      Logger.LogInformation($"{nameof(UserController)}.{nameof(CreateUser)}.End");

      return result;
    }

    private bool IsValidUserViewModel(UserViewModel user)
    {
      var isValid = true;
      if (!user.HumanColorTypeId.HasValue)
      {
        isValid = false;
        ModelState.AddModelError("HumanColorTypeId", "HumanColorTypeId is null.");
      }
      if (!user.ShapeTypeId.HasValue)
      {
        isValid = false;
        ModelState.AddModelError("ShapeTypeId", "ShapeTypeId is null.");
      }
      if (!user.SexTypeId.HasValue)
      {
        isValid = false;
        ModelState.AddModelError("SexTypeId", "SexTypeId is null.");
      }
      return isValid;
    }

    // For developers only
    [HttpGet("{userId}")]
    public IActionResult GetUser(int userId)
    {
      Logger.LogInformation($"{nameof(UserController)}.{nameof(GetUser)}.Start");
      if (userId == 0)
      {
        ModelState.AddModelError($"{nameof(userId)}", $"{nameof(userId)} equals 0. It's impossible.");
        return new BadResponseResult(ModelState);
      }

      var user = Db.GetUser(userId);

      var result = new OkResponseResult($"User", user);
      Logger.LogInformation($"{nameof(UserController)}.{nameof(GetUser)}.End");
      return result;
    }

    [HttpPut("{userId}")]
    public IActionResult UpdateUser(int userId, [FromQuery] UserViewModel viewModel)
    {
      Logger.LogInformation($"{nameof(UserController)}.{nameof(UpdateUser)}.Start");

      var oldUser = Db.GetUser(userId);
      var updatedUser = new UserEntity
      {
        FirstName = string.IsNullOrEmpty(viewModel.FirstName) ? oldUser.FirstName : viewModel.FirstName,
        LastName = string.IsNullOrEmpty(viewModel.LastName) ? oldUser.LastName : viewModel.LastName,
        SexTypeId = viewModel.SexTypeId ?? oldUser.SexType.SexTypeId,
        ShapeTypeId = viewModel.ShapeTypeId ?? oldUser.ShapeType.ShapeTypeId,
        HumanColorTypeId = viewModel.HumanColorTypeId ?? oldUser.HumanColorType.HumanColorTypeId,
      };

      var newUser = Db.UpdateUser(userId, updatedUser);

     var result = new UpdatedResponseResult("User shape was updated.", newUser);

      Logger.LogInformation($"{nameof(UserController)}.{nameof(UpdateUser)}.End");
     return result;
    }

    
  }
}