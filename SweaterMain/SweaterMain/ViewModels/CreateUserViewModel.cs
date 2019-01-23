using System.ComponentModel.DataAnnotations;

namespace SweaterMain.ViewModels
{
  public class CreateUserViewModel
  {
    [Required]
    [MaxLength(17, ErrorMessage = "The IMEI of the phone required no more than 17 numbers.")]
    [MinLength(15, ErrorMessage = "The IMEI of the phone required no less than 15 numbers.")]
    public string PhoneIMEI { get; set; }

    [Required]
    [MaxLength(6, ErrorMessage = "The web color has no more than 6 hex numbers.")]
    [MinLength(6, ErrorMessage = "The web color has no less than 6 hex numbers.")]
    public string EyeColor { get; set; }

    [Required]
    [MaxLength(6, ErrorMessage = "The web color has no more than 6 hex numbers.")]
    [MinLength(6, ErrorMessage = "The web color has no less than 6 hex numbers.")]
    public string HairColor { get; set; }

    [Required]
    [MaxLength(6, ErrorMessage = "The web color has no more than 6 hex numbers.")]
    [MinLength(6, ErrorMessage = "The web color has no less than 6 hex numbers.")]
    public string SkinTone { get; set; }
  }
}