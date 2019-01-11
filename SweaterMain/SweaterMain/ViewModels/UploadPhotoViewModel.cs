using Microsoft.AspNetCore.Http;

namespace ZebraMain.ViewModels
{
  public class UploadPhotoViewModel
  {
    public IFormFile File { get; set; }
    public string Url { get; set; }
  }
}