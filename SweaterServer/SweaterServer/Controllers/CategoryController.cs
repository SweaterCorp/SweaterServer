using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommonLibraries.CommonTypes;
using CommonLibraries.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductDatabase.Entities;
using ProductDatabase.Repositories;
using SweaterServer.ViewModels;

namespace SweaterServer.Controllers
{
  [Produces("application/json")]
  [EnableCors("AllowAllOrigin")]
  [Route("categories")]
  [ApiController]
  public class CategoryController : ControllerBase
  {
    private QueriesRepository Db { get; }
    private ILogger<CategoryController> Logger { get; }

    public CategoryController(ILogger<CategoryController> logger, QueriesRepository db)
    {
      Logger = logger;
      Db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
      Logger.LogInformation($"{nameof(CategoryController)}.{nameof(GetCategories)}.Start");

      var categories = await Db.GetCategoriesAsync();
      var result = new OkResponseResult("Categories", new {Counts = categories.Count, Categories = categories});

      Logger.LogInformation($"{nameof(CategoryController)}.{nameof(GetCategories)}.End");
      return result;
    }

    [HttpGet("{categoryId}/brands")]
    public async Task<IActionResult> GetBrands([FromRoute] [Range(1, long.MaxValue)] int categoryId)
    {
      Logger.LogInformation($"{nameof(CategoryController)}.{nameof(GetBrands)}.Start");

      var category = await Db.GetCategoryByIdAsync(categoryId);
      if (category == null)
        return new ResponseResult(HttpStatusCode.NotFound, $"There is no category with id:{categoryId}");

      var brands = await Db.GetCategoryBrandsAsync(categoryId);

      var result =
        new OkResponseResult($"Brands for category:{category.CategoryId} {(CategoryType) category.CategoryTypeId}.",
          new CategoryContainerViewModel<BrandEntity>(category, brands.Count, brands));

      Logger.LogInformation($"{nameof(CategoryController)}.{nameof(GetBrands)}.End");
      return result;
    }

    [HttpGet("{categoryId}/sizes")]
    public async Task<IActionResult> GetSizes([FromRoute] [Range(1, long.MaxValue)] int categoryId)
    {
      Logger.LogInformation($"{nameof(CategoryController)}.{nameof(GetSizes)}.Start");

      var category = await Db.GetCategoryByIdAsync(categoryId);
      if (category == null) return new NotFoundResponseResult($"There is no category with id:{categoryId}");

      var sizes = await Db.GetCategorySizesAsync(categoryId);

      var result =
        new OkResponseResult($"Sizes for category:{category.CategoryId} {(CategoryType) category.CategoryTypeId}.",
          new CategoryContainerViewModel<SizeTypeEntity>(category, sizes.Count, sizes));

      Logger.LogInformation($"{nameof(CategoryController)}.{nameof(GetSizes)}.Start");
      return result;
    }
  }
}