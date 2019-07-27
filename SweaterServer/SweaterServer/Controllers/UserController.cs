using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibraries.ColorAlgos;
using CommonLibraries.CommonTypes;
using CommonLibraries.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using ProductDatabase.DTOs;
using ProductDatabase.Repositories;
using SweaterServer.Models;
using SweaterServer.ViewModels;

namespace SweaterServer.Controllers
{
  /// <summary>
  /// Url path to work with user entities
  /// </summary>
  [Produces("application/json")]
  [EnableCors("AllowAllOrigin")]
  [Route("api/users")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private QueriesRepository QueriesDb { get; }
    private UserRepository UserDb { get; }
    private ILogger<UserController> Logger { get; }

    public UserController(ILogger<UserController> logger, QueriesRepository queriesDb, UserRepository userDb)
    {
      Logger = logger;
      QueriesDb = queriesDb;
      UserDb = userDb;
    }

    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns>Suitable products for the new user.</returns>
    /// <response code="200">Returns list of the suitable products.</response>
    /// <response code="400">If the params for creating are null.</response>        
    [HttpPost]
    [ProducesResponseType(typeof(ResponseObject<List<ProductViewModel>>), 200)]
    [ProducesResponseType(typeof(ResponseObject<ModelStateDictionary>), 400)]
    public async Task<IActionResult> CreateUser([FromBody] UserInfo userInfo)
    {
      Logger.LogInformation($"{nameof(UserController)}.{nameof(CreateUser)}.Start");
      if (!ModelState.IsValid) return new BadResponseResult(ModelState);

      var user = await UserDb.GetUser(userInfo.PhoneIMEI);
      if (user != null)
      {
        var personalTone = new PersonalColorTypeQualifier().GetPersonalColorType(userInfo.EyeColor, userInfo.HairColor,
            userInfo.SkinTone);
        user = await UserDb.CreateUser(userInfo.PhoneIMEI, personalTone);
      }

      Logger.LogInformation($"{nameof(UserController)}.{nameof(CreateUser)}.End");
      return new OkResponseResult("New user.", user);
    }

    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="createUser"></param>
    /// <returns>Suitable products for the new user.</returns>
    /// <response code="200">Returns list of the suitable products.</response>
    /// <response code="400">If the params for creating are null.</response>       
    [HttpPut]
    [ProducesResponseType(typeof(ResponseObject<List<ProductViewModel>>), 200)]
    [ProducesResponseType(typeof(ResponseObject<ModelStateDictionary>), 400)]
    public async Task<IActionResult> UpdateUser([FromBody] UserInfo createUser)
    {
      Logger.LogInformation($"{nameof(UserController)}.{nameof(UpdateUser)}.Start");
      if (!ModelState.IsValid) return new BadResponseResult(ModelState);

      var user = await UserDb.GetUser(createUser.PhoneIMEI);
      if (user == null) return new NotFoundResponseResult("The user with id doesn't exist");
      var personalTone = new PersonalColorTypeQualifier().GetPersonalColorType(createUser.EyeColor, createUser.HairColor, createUser.SkinTone);
      user.PersonalColorTypeId = (int) personalTone;
      user = await UserDb.UpdateUser(user);


      Logger.LogInformation($"{nameof(UserController)}.{nameof(UpdateUser)}.End");
      return new OkResponseResult("Updated user.", user);
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts([FromQuery] string phoneImei,
      [FromQuery] ProductFilter filter)
    {
      Logger.LogInformation($"{nameof(UserController)}.{nameof(GetProducts)}.Start");
      if (!ModelState.IsValid) return new BadResponseResult(ModelState);

      var user = await UserDb.GetUser(phoneImei);
      if (user == null) return new NotFoundResponseResult($"There is no user with phone:{phoneImei}");

      var result = await FitProducts(new ProductFilter
      {
        CategoryId = (int)CategoryType.BlousesShirts,
        PersonalColorTypeId = user.PersonalColorTypeId
      });

      Logger.LogInformation($"{nameof(UserController)}.{nameof(GetProducts)}.End");
      return new OkResponseResult("Fit products.", new { Products = result.Select(ProductViewModel.FromPrdouctDto) });
    }


    private async Task<List<ProductCardDto>> FitProducts(ProductFilter productFilterViewModel)
    {
      var filter = new ProductFilterDto
      {
        CategoryId = productFilterViewModel.CategoryId,
        BrandIds = productFilterViewModel.BrandsIds,
        MinimalPrice = productFilterViewModel.MinimalPrice,
        MaximalPrice = productFilterViewModel.MaximalPrice,
        PersonalColorType = (PersonalColorType)productFilterViewModel.PersonalColorTypeId,
        SizeIds = productFilterViewModel.SizesIds
      };
      (int counts, List<ProductCardDto> list) = await QueriesDb.SelectProductsAsync(filter,
        productFilterViewModel.PageParams.Offset, productFilterViewModel.PageParams.Count);
      return list;
    }
  }
}