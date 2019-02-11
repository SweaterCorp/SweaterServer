using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibraries.CommonTypes;
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
  [Route("api/categories")]
  [ApiController]
  public class CategoryController : ControllerBase
  {
    private QueriesRepository Db { get; }
    private ColorGoodnessRepository ColorDb { get; }
    private ILogger<CategoryController> Logger { get; }

    public CategoryController(ILogger<CategoryController> logger, QueriesRepository db, ColorGoodnessRepository colorDb)
    {
      Logger = logger;
      Db = db;
      ColorDb = colorDb;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
      Logger.LogInformation($"{nameof(CategoryController)}.{nameof(GetCategories)}.Start");

      var categories = await Db.GetCategoriesAsync();
      var result = new OkResponseResult("Categories", new { Counts = categories.Count, Categories = categories });

      Logger.LogInformation($"{nameof(CategoryController)}.{nameof(GetCategories)}.End");
      return result;
    }

    [HttpGet("{categoryId}/brands")]
    public async Task<IActionResult> GetBrands(int categoryId)
    {
      Logger.LogInformation($"{nameof(CategoryController)}.{nameof(GetBrands)}.Start");

      var category = await Db.GetCategoryByIdAsync(categoryId);
      if (category == null) return new ResponseResult(HttpStatusCode.NotFound, $"There is no category with id:{categoryId}");

      var brands = await Db.GetCategoryBrandsAsync(categoryId);

      var result = new OkResponseResult($"Brands for category:{category.CategoryId} {((CategoryType)category.CategoryTypeId).Name}.",
        new CategoryContainerViewModel<BrandEntity>(category, brands.Count, brands));

      Logger.LogInformation($"{nameof(CategoryController)}.{nameof(GetBrands)}.End");
      return result;
    }

    [HttpGet("{categoryId}/sizes")]
    public async Task<IActionResult> GetSizes(int categoryId)
    {
      Logger.LogInformation($"{nameof(CategoryController)}.{nameof(GetSizes)}.Start");

      var category = await Db.GetCategoryByIdAsync(categoryId);
      if (category == null) return new NotFoundResponseResult($"There is no category with id:{categoryId}");

      var sizes = await Db.GetCategorySizesAsync(categoryId);

      var result = new OkResponseResult($"Sizes for category:{category.CategoryId} {((CategoryType)category.CategoryTypeId).Name}.",
        new CategoryContainerViewModel<SizeTypeEntity>(category, sizes.Count, sizes));

      Logger.LogInformation($"{nameof(CategoryController)}.{nameof(GetSizes)}.Start");
      return result;
    }
  }
}