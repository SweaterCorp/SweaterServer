using System.IO;
using System.Net;
using CommonLibraries.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZebraData.Entities.UserGroup;
using ZebraData.Repositories;
using ZebraMain.Infrastructure;
using ZebraMain.ViewModels;

namespace ZebraMain.Controllers
{
  [Produces("application/json")]
  [EnableCors("AllowAllOrigin")]
  [Route("api/users")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private const string Host = "http://Zebra.com/";

    private UserRepository Db { get; }
    private MediaService MediaService { get; }
    private ILogger<UserController> Logger { get; }

    private MediaSettings MediaSettings { get; }
    private IHostingEnvironment AppEnvironment { get; }

    public UserController(ILogger<UserController> logger, MediaService mediaService, UserRepository db,
      IOptions<MediaSettings> options, IHostingEnvironment appEnvironment)
    {
      Logger = logger;
      Db = db;
      MediaService = mediaService;
      MediaSettings = options.Value;
      AppEnvironment = appEnvironment;
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserViewModel viewModel)
    {
      Logger.LogInformation($"{nameof(UserController)}.{nameof(CreateUser)}.Start");

      if (!IsValidUserViewModel(viewModel)) return new BadResponseResult(ModelState);

      var user = Db.CreateUser(new UserEntity
      {
        FirstName = viewModel.FirstName,
        HumanColorTypeId = viewModel.HumanColorTypeId.GetValueOrDefault(),
        LastName = viewModel.LastName,
        SexTypeId = viewModel.SexTypeId.GetValueOrDefault(),
        ShapeTypeId = viewModel.ShapeTypeId.GetValueOrDefault()
      });
      var result = new ResponseResult(HttpStatusCode.Created, $"User was created with userId: {user.UserId}.",
        new {User = user});

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

    [HttpPut("{userId}")]
    public IActionResult UpdateUser(int userId, [FromQuery] UserViewModel viewModel)
    {
      Logger.LogInformation($"{nameof(UserController)}.{nameof(UpdateUser)}.Start");

      var oldUser = Db.GetUser(userId);
      if (oldUser.UserId == 0) return new NotFoundResponseResult($"There is no user with userId: {userId}");

      var updatedUser = new UserEntity
      {
        FirstName = string.IsNullOrEmpty(viewModel.FirstName) ? oldUser.FirstName : viewModel.FirstName,
        LastName = string.IsNullOrEmpty(viewModel.LastName) ? oldUser.LastName : viewModel.LastName,
        SexTypeId = viewModel.SexTypeId ?? oldUser.SexType.SexTypeId,
        ShapeTypeId = viewModel.ShapeTypeId ?? oldUser.ShapeType.ShapeTypeId,
        HumanColorTypeId = viewModel.HumanColorTypeId ?? oldUser.HumanColorType.HumanColorTypeId
      };

      var newUser = Db.UpdateUser(userId, updatedUser);

      var result = new UpdatedResponseResult($"User shape was updated to userId: {userId}.", newUser);

      Logger.LogInformation($"{nameof(UserController)}.{nameof(UpdateUser)}.End");
      return result;
    }

    [HttpPost("{userId}/photos")]
    public IActionResult UploadUserPhotoViaFile(int userId, [FromBody] UploadPhotoViewModel viewModel)
    {
      return UploadUserPhoto(userId, viewModel.File, viewModel.Url);
    }

    [HttpPost("{userId}/photos/url")]
    public IActionResult UploadUserPhotoViaUrl(int userId, [FromBody] UploadPhotoViewModel viewModel)
    {
      return UploadUserPhoto(userId, viewModel.File, viewModel.Url);
    }

    public IActionResult UploadUserPhoto(int userId, IFormFile file, string url)
    {
      Logger.LogInformation($"{nameof(MonitoringController)}.{nameof(UploadUserPhoto)}.Start");

      var user = Db.GetUser(userId);
      if (user.UserId == 0) return new NotFoundResponseResult($"There is no user with userId: {userId}");

      ServerUrl photoUrl;
      if (!string.IsNullOrEmpty(url))
      {
        photoUrl = MediaService.UploadPhotoViaUrl(url);
      }
      else if (file != null)
      {
        photoUrl = MediaService.UploadPhotoViaFile(file);
      }
      else
      {
        ModelState.AddModelError("Url", "Url is null or empty.");
        ModelState.AddModelError("File", "File is null.");
        return new BadResponseResult(ModelState);
      }

      if (ServerUrl.IsNullOrEmpty(photoUrl))
        return new ResponseResult(HttpStatusCode.NotModified, $"Photo was not added to userId: {userId}.",
          new {User = user});

      var userPhoto = Db.InsertUserPhoto(userId, photoUrl.ToWebUrl());
      ResponseResult result;
      if (userPhoto == null)
      {
        result = new ResponseResult(HttpStatusCode.NotModified, $"Photo was not added to userId: {userId}.",
          new {User = user});
      }
      else
      {
        userPhoto.PhotoUrl = ToWebPath(userPhoto.PhotoUrl);
        result = new OkResponseResult($"Photo was added to user id:{userId}.", new {User = user, Photo = userPhoto});
      }

      Logger.LogInformation($"{nameof(MonitoringController)}.{nameof(UploadUserPhoto)}.End");
      return result;
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
      if (user.UserId == 0) return new NotFoundResponseResult($"There is no user with userId: {userId}");

      var result = new OkResponseResult($"User for userId:{userId}", user);
      Logger.LogInformation($"{nameof(UserController)}.{nameof(GetUser)}.End");
      return result;
    }

    // For developers only
    [HttpGet("{userId}/photos")]
    public IActionResult GetUserPhotos(int userId)
    {
      Logger.LogInformation($"{nameof(UserController)}.{nameof(GetUserPhotos)}.Start");
      if (userId == 0)
      {
        ModelState.AddModelError($"{nameof(userId)}", $"{nameof(userId)} equals 0. It's impossible.");
        return new BadResponseResult(ModelState);
      }

      var (user, photos) = Db.GetUserWithPhotos(userId);
      if (user.UserId == 0) return new NotFoundResponseResult($"There is no user with userId: {userId}");

      foreach (var photo in photos) photo.PhotoUrl = ToWebPath(photo.PhotoUrl);

      var result = new OkResponseResult($"User for userId:{userId}", new {User = user, Photos = photos});
      Logger.LogInformation($"{nameof(UserController)}.{nameof(GetUserPhotos)}.End");
      return result;
    }

    private string ToPcPath(string url)
    {
      var folder = Path.Combine(AppEnvironment.ContentRootPath, MediaSettings.RelativePath, MediaSettings.RootFolder);
      return Path.Combine(folder, new ServerUrl(url).ToPcUrl());
    }

    private string ToWebPath(string url)
    {
      return Host + "/" + new ServerUrl(url).ToWebUrl();
    }
  }
}