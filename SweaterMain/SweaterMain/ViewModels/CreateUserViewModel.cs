using System.ComponentModel.DataAnnotations;

namespace SweaterMain.ViewModels
{
  public class CreateUserViewModel
  {
    [Required]
    [Range(15, 17, ErrorMessage = "The IMEI of the phone required no less than 15 and no more than 17 numbers.")]
    public string PhoneIMEI { get; set; }

    [Required]
    [Range(6,6, ErrorMessage = "The web color has 6 hex numbers.")]
    public string EyeColor { get; set; }

    [Required]
    [Range(6, 6, ErrorMessage = "The web color has 6 hex numbers.")]
    public string HairColor { get; set; }

    [Required]
    [Range(6, 6, ErrorMessage = "The web color has 6 hex numbers.")]
    public string SkinTone { get; set; }
  }
}