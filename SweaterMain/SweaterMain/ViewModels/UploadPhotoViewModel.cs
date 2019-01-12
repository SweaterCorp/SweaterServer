using Microsoft.AspNetCore.Http;

namespace SweaterMain.ViewModels
{
  public class UploadPhotoViewModel
  {
    public IFormFile File { get; set; }
    public string Url { get; set; }
  }
}