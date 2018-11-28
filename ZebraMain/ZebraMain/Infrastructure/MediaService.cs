using System;
using System.IO;
using System.Net;
using CommonLibraries.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ZebraMain.Infrastructure
{
  public class MediaService
  {
    private MediaSettings MediaSettings { get; }
    private IHostingEnvironment AppEnvironment { get; }
    private string FolderPath { get; }

    public MediaService(IOptions<MediaSettings> options, IHostingEnvironment appEnvironment)
    {
      MediaSettings = options.Value;
      AppEnvironment = appEnvironment;
      FolderPath  = Path.Combine(AppEnvironment.ContentRootPath, MediaSettings.RelativePath,
        MediaSettings.RootFolder);
    }

    public ServerUrl UploadPhotoViaFile(IFormFile file)
    {
      var result = UploadPhoto(file.FileName, filePath =>
      {
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
          file.CopyTo(fileStream);
        }
      });

      return result;
    }

    public ServerUrl UploadPhotoViaUrl(string url)
    {
      var heasers = new WebHeaderCollection {"User-Agent: Other"};
      var client = new WebClient {Headers = heasers};

      var result = UploadPhoto(url, filePath => client.DownloadFile(new Uri(url), filePath));

      return result;
    }

    private ServerUrl UploadPhoto(string fileName, Action<string> saveMethod)
    {
      var uniqueName = CreateUniqueName(fileName);
      var imageRelativePath = "/" + uniqueName;
      var url = new ServerUrl(imageRelativePath);

      var fullpath = Path.Combine(FolderPath, url.ToPcUrl());

      saveMethod.Invoke(fullpath);

      return url;
    }

    private string CreateUniqueName(string imageName)
    {
      var ext = Path.GetExtension(imageName);
      if (ext.IsNullOrEmpty()) ext = imageName.ToLower().Contains(".png") ? ".png" : ".jpg";
      else ext = ext.Substring(0, 4);
      if (ext == ".jpe") ext = ".jpg";
      return imageName.GetMd5HashString().Substring(0, 2) +
             Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10) + ext;
    }
  }
}