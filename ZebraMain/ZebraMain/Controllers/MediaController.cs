using System;
using System.Collections.Generic;
using System.Net;
using CommonLibraries.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZebraData;
using ZebraData.Entities.ProductGroup;
using ZebraData.Repositories;
using ZebraMain.ViewModels;

namespace ZebraMain.Controllers
{
  [Route("api/media")]
  [ApiController]
  public class MediaController : ControllerBase
  {
    private readonly ProductRepository _db;
    private readonly IHostingEnvironment _appEnvironment;

    public MediaController(IHostingEnvironment appEnvironment, ProductRepository db)
    {
      _db = db;
      _appEnvironment = appEnvironment;
    }

    
    [HttpPost("media/photo/file/upload")]
    public IActionResult UploadUserPhotoViaFile(UploadPhotoViewModel uploadPhoto)
    {
      
      return new OkResponseResult($"hello", _appEnvironment);
    }

    [HttpGet("media/photo/url/upload")]
    public IActionResult UploadUserPhotoViaUrl([FromQuery] int userId, [FromQuery] string url)
    {
      return new OkResponseResult($"hello", _appEnvironment);
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