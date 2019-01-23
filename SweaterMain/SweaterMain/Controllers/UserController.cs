using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CommonLibraries.ColorAlgos;
using CommonLibraries.CommonTypes;
using CommonLibraries.Infrastructures.ColorsData;
using CommonLibraries.Resources;
using CommonLibraries.Resources.DeserializerTypes;
using CommonLibraries.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductDatabase.DTOs;
using ProductDatabase.Entities;
using ProductDatabase.Repositories;
using SweaterMain.ViewModels;

namespace SweaterMain.Controllers
{
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


    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserViewModel createUser)
    {
      if (!ModelState.IsValid) return new BadResponseResult(ModelState);

      var user = await UserDb.GetUser(createUser.PhoneIMEI);
      if (user == null)
      {
        var personalTone = new PersonalColorTypeQualifier().GetPersonalColorType(createUser.EyeColor, createUser.HairColor, createUser.SkinTone);
        user = await UserDb.CreateUser(createUser.PhoneIMEI, personalTone);
      }


      var result = await FitProducts(new ProductFilterViewModel(){ CategoryId = CategoryType.BlousesShirts.Id, PersonalColorTypeId = user.PersonalColorTypeId});

      return new OkResponseResult("result is :",result);

    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts([FromQuery] string phoneIMEI, [FromQuery] ProductFilterViewModel getProducts)
    {
      if (!ModelState.IsValid) return new BadResponseResult(ModelState);

      var user = await UserDb.GetUser(phoneIMEI);
      if (user == null)
      {
        return new NotFoundResponseResult($"There is no user with phone:{phoneIMEI}");
      }


      var result = await FitProducts(new ProductFilterViewModel() { CategoryId = CategoryType.BlousesShirts.Id, PersonalColorTypeId = user.PersonalColorTypeId });

      return new OkResponseResult("result is :", result);

    }

    public async Task<List<ProductCardDto>> FitProducts(ProductFilterViewModel productFilterViewModel)
    {
      var filter = new ProductsFilterDto
      {
        CategoryId = productFilterViewModel.CategoryId,
        BrandIds = productFilterViewModel.BrandsIds,
        MinimalPrice = productFilterViewModel.MinimalPrice,
        MaximalPrice = productFilterViewModel.MaximalPrice,
        PersonalColorType = (PersonalColorType) productFilterViewModel.PersonalColorTypeId,
        SizeIds = productFilterViewModel.SizesIds
      };
      (int counts, List<ProductCardDto> list) =  await QueriesDb.SelectProductsAsync(filter, productFilterViewModel.PageParams.Offset,
        productFilterViewModel.PageParams.Count);
      return list;
    }

    
    
  }
}