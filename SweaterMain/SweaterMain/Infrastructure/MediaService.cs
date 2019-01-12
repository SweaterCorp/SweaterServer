using System;
using System.IO;
using System.Net;
using CommonLibraries.CommonTypes;
using CommonLibraries.Extensions;
using CommonLibraries.Infrastructures;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace SweaterMain.Infrastructure
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
      var fileName = CreateFullFileName(file.Name);
      var originalfileName = AddSizeTypeToPhotoPath(fileName, PhotoSizeType.Original);

      using (var fileStream = new FileStream(originalfileName, FileMode.Create))
      {
        file.CopyTo(fileStream);
      }

      return new ServerUrl(originalfileName);
    }

    public ServerUrl UploadPhotoViaUrl(string url)
    {
      var heasers = new WebHeaderCollection {"User-Agent: Other"};
      var client = new WebClient {Headers = heasers};

      var fileName = CreateFullFileName(url);
      var originalfileName = AddSizeTypeToPhotoPath(fileName, PhotoSizeType.Original);

      client.DownloadFile(new Uri(url), originalfileName);

      return new ServerUrl(originalfileName);
    }

    private static string AddSizeTypeToPhotoPath(string photoPath, PhotoSizeType photoSizeType)
    {
      var ext = Path.GetExtension(photoPath);
      var name = Path.GetFileNameWithoutExtension(photoPath);
      var folderName = Path.GetDirectoryName(photoPath);
      return Path.Combine(folderName, $"{name}_{photoSizeType.ToString().ToLower()}{ext}");
    }

    private string CreateFullFileName(string fileName)
    {
      var uniqueName = CreateUniqueName(fileName);
      var imageRelativePath = "/" + uniqueName;
      var url = new ServerUrl(imageRelativePath);

      var fullpath = Path.Combine(FolderPath, url.ToPcUrl());

      return fullpath;
    }

    

    private static string CreateUniqueName(string imageName)
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