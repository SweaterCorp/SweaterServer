using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ZebraMain.ViewModels
{
  public class UploadPhotoViewModel
  {
    [Required]
    public int UserId { get; set; }

    [Required]
    public IFormFile File { get; set; }
  }
}