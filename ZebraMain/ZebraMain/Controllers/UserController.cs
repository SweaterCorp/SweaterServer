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
    public IActionResult CreateUser([FromBody] CreateUserViewModel viewModel)
    {
      Logger.LogInformation($"{nameof(UserController)}.{nameof(CreateUser)}.Start");

      if (!ModelState.IsValid) return new BadResponseResult(ModelState);

      var user = Db.CreateUser(new UserEntity
      {
        BirthDate = viewModel.BirthDate,
        FirstName = viewModel.FirstName,
        HumanColorTypeId = viewModel.HumanColorTypeId,
        LastName = viewModel.LastName,
        SexTypeId = viewModel.SexTypeId,
        ShapeTypeId = viewModel.ShapeTypeId,
        UserId = viewModel.UserId
      });
      var result = new ResponseResult(HttpStatusCode.Created, $"User was created.", new {User = user});

      Logger.LogInformation($"{nameof(UserController)}.{nameof(CreateUser)}.End");

      return result;
    }

    // For developers only
    [HttpGet("{userId}")]
    public IActionResult GetUser(int userId)
    {
      Logger.LogInformation($"{nameof(UserController)}.{nameof(GetUser)}.Start");
      if (userId == 0) return Redirect("https://yandex.ru/images/search?text=сюрприз");

      var user = Db.GetUser(userId);

      var result = new OkResponseResult($"User", user);
      Logger.LogInformation($"{nameof(UserController)}.{nameof(GetUser)}.End");
      return result;
    }

    [HttpPatch("{userId}/shape_type")]
    public IActionResult UpdateUserShapeType(int userId, [FromQuery] int shapeTypeId)
    {
      Logger.LogInformation($"{nameof(UserController)}.{nameof(UpdateUserShapeType)}.Start");

      if (userId == 0)
      {
        ModelState.AddModelError($"{nameof(userId)}", $"{nameof(userId)} equals 0. It's impossible.");
        return new BadResponseResult(ModelState);
      }

      if (shapeTypeId == 0)
      {
        ModelState.AddModelError($"{nameof(shapeTypeId)}", $"{nameof(shapeTypeId)} equals 0. It's impossible.");
        return new BadResponseResult(ModelState);
      }

      var newUser = Db.UpdateUserType(userId, shapeTypeId, UserRepository.UpdatedType.Shape);

      var result = new UpdatedResponseResult("User shape was updated.", new {User = newUser});

      Logger.LogInformation($"{nameof(UserController)}.{nameof(UpdateUserShapeType)}.End");
      return result;
    }

    [HttpPatch("{userId}/human_color_type")]
    public IActionResult UpdateHumanColorType(int userId, [FromQuery] int humanColorTypeId)
    {
      Logger.LogInformation($"{nameof(UserController)}.{nameof(UpdateHumanColorType)}.Start");

      if (userId == 0)
      {
        ModelState.AddModelError($"{nameof(userId)}", $"{nameof(userId)} equals 0. It's impossible.");
        return new BadResponseResult(ModelState);
      }

      if (humanColorTypeId == 0)
      {
        ModelState.AddModelError($"{nameof(humanColorTypeId)}",
          $"{nameof(humanColorTypeId)} equals 0. It's impossible.");
        return new BadResponseResult(ModelState);
      }

      var newUser = Db.UpdateUserType(userId, humanColorTypeId, UserRepository.UpdatedType.HumanColor);

      var result = new UpdatedResponseResult("User was updated.", new {User = newUser});

      Logger.LogInformation($"{nameof(UserController)}.{nameof(UpdateHumanColorType)}.End");
      return result;
    }

    [HttpPatch("{userId}/sex_type")]
    public IActionResult UpdateUserSexType(int userId, [FromQuery] int sexTypeId)
    {
      Logger.LogInformation($"{nameof(UserController)}.{nameof(UpdateUserSexType)}.Start");

      if (userId == 0)
      {
        ModelState.AddModelError($"{nameof(userId)}", $"{nameof(userId)} equals 0. It's impossible.");
        return new BadResponseResult(ModelState);
      }

      if (sexTypeId == 0)
      {
        ModelState.AddModelError($"{nameof(sexTypeId)}", $"{nameof(sexTypeId)} equals 0. It's impossible.");
        return new BadResponseResult(ModelState);
      }

      var newUser = Db.UpdateUserType(userId, sexTypeId, UserRepository.UpdatedType.Sex);
      var result = new UpdatedResponseResult("User sex type was updated.", new {User = newUser});

      Logger.LogInformation($"{nameof(UserController)}.{nameof(UpdateUserSexType)}.End");
      return result;
    }
  }
}