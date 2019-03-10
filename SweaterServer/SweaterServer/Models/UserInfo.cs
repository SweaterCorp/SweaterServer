using System.ComponentModel.DataAnnotations;

namespace SweaterServer.Models
{
  public class UserInfo
  {
    [Required]
    [StringLength(15, MinimumLength = 17, ErrorMessage = "The IMEI of the phone required no less than 15 and no more than 17 numbers.")] // ReSharper disable All
    public string PhoneIMEI { get; set; }

    [Required]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "The web color has 6 hex numbers.")]
    public string EyeColor { get; set; }

    [Required]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "The web color has 6 hex numbers.")]
    public string HairColor { get; set; }

    [Required]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "The web color has 6 hex numbers.")]
    public string SkinTone { get; set; }
  }
}