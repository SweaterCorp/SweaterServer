using System.Collections.Generic;
using System.Linq;
using System.Net;
using CommonLibraries.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SweaterData;
using SweaterData.DTOs;
using SweaterData.Entities.ProductGroup;
using SweaterData.Repositories;
using SweaterMain.ViewModels;

namespace SweaterMain.Controllers
{
  [Produces("application/json")]
  [EnableCors("AllowAllOrigin")]
  [Route("api/categories")]
  [ApiController]
  public class MarketController : ControllerBase
  {
    private ProductRepository Db { get; }
    private ILogger<MarketController> Logger { get; }

    public MarketController(ILogger<MarketController> logger, ProductRepository db)
    {
      Logger = logger;
      Db = db;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
      Logger.LogInformation($"{nameof(MarketController)}.{nameof(GetCategories)}.Start");

      var categories = Db.GetCategories();
      var result =  new OkResponseResult("Categories", new {Counts = categories.Count, Categories = categories});

      Logger.LogInformation($"{nameof(MarketController)}.{nameof(GetCategories)}.End");
      return result;
    }

    [HttpGet("{categoryId}/brands")]
    public IActionResult GetBrands(int categoryId)
    {
      Logger.LogInformation($"{nameof(MarketController)}.{nameof(GetBrands)}.Start");

      var category = Db.GetCategoryById(categoryId);
      if (category == null) return new ResponseResult(HttpStatusCode.NotFound, $"There is no category with id:{categoryId}");

      var brands = Db.GetCategoryBrands(categoryId);

      var result =  new OkResponseResult($"Brands for category:{category.CategoryId} {category.RussianName}.",
        new CategoryContainerViewModel<BrandEntity>(category, brands.Count, brands));

      Logger.LogInformation($"{nameof(MarketController)}.{nameof(GetBrands)}.End");
      return result;
    }

    [HttpGet("{categoryId}/colors")]
    public IActionResult GetColors(int categoryId)
    {
      Logger.LogInformation($"{nameof(MarketController)}.{nameof(GetBrands)}.Start");
      var category = Db.GetCategoryById(categoryId);

      if (category == null) return new NotFoundResponseResult($"There is no category with id:{categoryId}");

      var colors = Db.GetCategoryColors(categoryId);

      var result =  new OkResponseResult($"Colors for category:{category.CategoryId} {category.RussianName}.",
        new CategoryContainerViewModel<ColorTypeEntity>(category, colors.Count, colors));
      Logger.LogInformation($"{nameof(MarketController)}.{nameof(GetBrands)}.Start");
      return result;
    }

    [HttpGet("{categoryId}/sizes")]
    public IActionResult GetSizes(int categoryId)
    {
      Logger.LogInformation($"{nameof(MarketController)}.{nameof(GetBrands)}.Start");
      var category = Db.GetCategoryById(categoryId);

      if (category == null) return new NotFoundResponseResult($"There is no category with id:{categoryId}");

      var sizes = Db.GetCategorySizes(categoryId);

      var result =  new OkResponseResult($"Sizes for category:{category.CategoryId} {category.RussianName}.",
        new CategoryContainerViewModel<SizeTypeEntity>(category, sizes.Count, sizes));
      Logger.LogInformation($"{nameof(MarketController)}.{nameof(GetBrands)}.Start");
      return result;
    }

    [HttpGet("{categoryId}/products")]
    public IActionResult SelectProduct(int categoryId, [FromQuery] ProductsFilterViewModel filter)
    {
      Logger.LogInformation($"{nameof(MarketController)}.{nameof(GetBrands)}.Start");
      var category = Db.GetCategoryById(categoryId);

      if (category == null) return new NotFoundResponseResult($"There is no category with id:{categoryId}");

      (int counts, List<ProductCardDto> list) =
        Db.SelectProducts(
          new ProductsFilterDto
          {
            CategoryId = categoryId,
            MinimalPrice = filter.MinimalPrice,
            MaximalPrice = filter.MaximalPrice,
            BrandsIds = filter.BrandsIds,
            ColorsIds = filter.ColorsIds,
            SizesIds = filter.SizesIds
          }, filter.PageParams.Offset, filter.PageParams.Count);

      var result =  new OkResponseResult($"Products for category:{category.CategoryId} {category.RussianName}.",
        new {Category = category, AllCounts = counts, Data = new {Counts = list.Count, Products = list}});
      Logger.LogInformation($"{nameof(MarketController)}.{nameof(GetBrands)}.Start");
      return result;
    }
  }
}