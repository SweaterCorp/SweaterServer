using System.Collections.Generic;
using CommonLibraries.Response;
using Microsoft.AspNetCore.Mvc;
using ZebraData;
using ZebraData.Entities.ProductGroup;
using ZebraData.Repositories;
using ZebraMain.ViewModels;

namespace ZebraMain.Controllers
{
  [Route("api/market")]
  [ApiController]
  public class MarketController : ControllerBase
  {
    private readonly ProductRepository _db;

    public MarketController(ProductRepository db)
    {
      _db = db;
    }

    [HttpGet("categories")]
    public IActionResult GetCategories()
    {
      var categories = _db.GetCategories();
      return new OkResponseResult("Categories", new {Counts = categories.Count, Categories = categories});
    }

    [HttpGet("categories/{categoryId}/brands")]
    public IActionResult GetBrands(int categoryId)
    {
      var category = _db.GetCategoryById(categoryId);

      var brands = _db.GetCategoryBrands(categoryId);

      return new OkResponseResult($"Brands for category:{category.CategoryId} {category.RussianName}.",
        new CategoryContainerViewModel<BrandEntity>(category, brands.Count, brands));
    }

    [HttpGet("categories/{categoryId}/colors")]
    public IActionResult GetColors(int categoryId)
    {
      var category = _db.GetCategoryById(categoryId);

      var colors = _db.GetCategoryColors(categoryId);

      return new OkResponseResult($"Colors for category:{category.CategoryId} {category.RussianName}.",
        new CategoryContainerViewModel<ColorTypeEntity>(category, colors.Count, colors));
    }

    [HttpGet("categories/{categoryId}/sizes")]
    public IActionResult GetSizes(int categoryId)
    {
      var category = _db.GetCategoryById(categoryId);

      var sizes = _db.GetCategorySizes(categoryId);

      return new OkResponseResult($"Sizes for category:{category.CategoryId} {category.RussianName}.",
        new CategoryContainerViewModel<SizeTypeEntity>(category, sizes.Count, sizes));
    }

    [HttpGet("categories/{categoryId}/products")]
    public IActionResult SelectProduct(int categoryId, [FromQuery] ProductsFilterViewModel filter)
    {
      var category = _db.GetCategoryById(categoryId);

      (int counts, List<ProductCardDto> list) =
        _db.SelectProducts(
          new ProductsFilterDto
          {
            CategoryId = categoryId,
            MinimalPrice = filter.MinimalPrice,
            MaximalPrice = filter.MaximalPrice,
            BrandsIds = filter.BrandsIds,
            ColorsIds = filter.ColorsIds,
            SizesIds = filter.SizesIds
          }, filter.PageParams.Offset, filter.PageParams.Count);

      return new OkResponseResult($"Products for category:{category.CategoryId} {category.RussianName}.",
        new {Category = category, AllCounts = counts, Data = new {Counts = list.Count, Products = list}});
    }
  }
}