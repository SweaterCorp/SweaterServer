using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ZebraMain.ViewModels
{
  public class CreateUpdateUserViewModel
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? HumanColorType { get; set; }
    public int? ShapeType { get; set; }
    public int? SexType { get; set; }

    //private bool IsValidUserViewModel(UserViewModel user)
    //{
    //  var isValid = true;
    //  if (!user.HumanColorType.HasValue)
    //  {
    //    isValid = false;
    //    ModelState.AddModelError("HumanColorTypeId", "HumanColorTypeId is null.");
    //  }
    //  if (!user.ShapeType.HasValue)
    //  {
    //    isValid = false;
    //    ModelState.AddModelError("ShapeTypeId", "ShapeTypeId is null.");
    //  }
    //  if (!user.SexType.HasValue)
    //  {
    //    isValid = false;
    //    ModelState.AddModelError("SexTypeId", "SexTypeId is null.");
    //  }
    //  return isValid;
    //}

    //public IEnumerable<ValidationResult> Validate(ModelStateDictionary modelState)
    //{

    //  if (HumanColorType.HasValue && !CommonLibraries.CommonTypes.HumanColorType.IsValid(HumanColorType.Value))
    //    errors.Add(new ValidationResult($"There is no human color type with id:{HumanColorType.Value}",
    //      new List<string> { "HumanColorType" }));
    //  if (ShapeType.HasValue && !CommonLibraries.CommonTypes.ShapeType.IsValid(ShapeType.Value))
    //    errors.Add(new ValidationResult($"There is no shape type with id:{ShapeType.Value}",
    //      new List<string> { "HumanColorType" }));
    //  if (SexType.HasValue && !CommonLibraries.CommonTypes.SexType.IsValid(SexType.Value))
    //    errors.Add(new ValidationResult($"There is no sex type with id:{SexType.Value}",
    //      new List<string> { "HumanColorType" }));

    //}

    public void Validate(ModelStateDictionary modelState)
    {
      if (HumanColorType.HasValue && !CommonLibraries.CommonTypes.HumanColorType.IsValid(HumanColorType.Value))
        modelState.AddModelError("HumanColorType", $"There is no human color type with id:{HumanColorType.Value}");
      if (ShapeType.HasValue && !CommonLibraries.CommonTypes.ShapeType.IsValid(ShapeType.Value))
        modelState.AddModelError("ShapeType", $"There is no shape type with id:{ShapeType.Value}");
      if (SexType.HasValue && !CommonLibraries.CommonTypes.SexType.IsValid(SexType.Value))
        modelState.AddModelError("SexType", $"There is no sex type with id:{SexType.Value}");
    }
  }
}