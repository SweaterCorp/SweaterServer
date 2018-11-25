using CommonLibraries.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZebraData.Repositories;
using ZebraMain.ViewModels;

namespace ZebraMain.Controllers
{
  [Produces("application/json")]
  [EnableCors("AllowAllOrigin")]
  [Route("api/media")]
  [ApiController]
  public class MediaController : ControllerBase
  {
    private ProductRepository Db { get; }
    private IHostingEnvironment AppEnvironment { get; }
    private ILogger<UserController> Logger { get; }

    public MediaController(ILogger<UserController> logger, IHostingEnvironment appEnvironment, ProductRepository db)
    {
      Db = db;
      AppEnvironment = appEnvironment;
      Logger = logger;
    }


    [HttpPost("media/photo/file/upload")]
    public IActionResult UploadUserPhotoViaFile(UploadPhotoViewModel uploadPhoto)
    {

      return new OkResponseResult($"hello", AppEnvironment);
    }

    [HttpGet("media/photo/url/upload")]
    public IActionResult UploadUserPhotoViaUrl([FromQuery] int userId, [FromQuery] string url)
    {
      return new OkResponseResult($"hello", AppEnvironment);
      //if (!ModelState.IsValid) return new BadResponseResult(ModelState);

      //if (MediaService.IsAlreadyDownloaded(background.Url))
      //  return new OkResponseResult(new UrlViewModel { Url = new Uri(MediaConverter.ToFullBackgroundurlUrl(background.Url, BackgroundSizeType.Original)).LocalPath });


      //var url = MediaService.UploadBackground(background.Url, background.BackgroundType)
      //  .FirstOrDefault(x => x.Size == BackgroundSizeType.Original)?.Url;
      //return url.IsNullOrEmpty()
      //  ? new ResponseResult((int)HttpStatusCode.NotModified)
      //  : new OkResponseResult(new UrlViewModel { Url = url });
    }


  }
}