using CommonLibraries.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using ZebraData;
using ZebraData.Entities.ProductGroup;
using ZebraData.Repositories;
using ZebraMain.ViewModels;

namespace ZebraMain.Controllers
{
  [Route("api")]
  [ApiController]
  public class GeneralController : ControllerBase
  {
    private ProductRepository _db;

    public GeneralController(ProductRepository db)
    {
      _db = db;
    }

    [HttpGet("market/categories")]
    public IActionResult GetCategories()
    {
      var categories = _db.GetCategories();
      return new OkResponseResult("Categories", new { Counts = categories.Count, Categories = categories });
    }

    [HttpGet("market/categories/{categoryId}/brands")]
    public IActionResult GetBrands(int categoryId)
    {
      var category = _db.GetCategoryById(categoryId);

      var brands = _db.GetCategoryBrands(categoryId);

      return new OkResponseResult($"Brands for {JsonConvert.SerializeObject(category)}", new { Counts = brands.Count, Brands = brands });
    }

    [HttpGet("market/categories/{categoryId}/colors")]
    public IActionResult GetColors(int categoryId)
    {
      var category = _db.GetCategoryById(categoryId);

      var colors = _db.GetCategoryColors(categoryId);

      return new OkResponseResult($"Colors for {JsonConvert.SerializeObject(category)}",
        new { Counts = colors.Count, Colors = colors });
    }

    [HttpGet("market/categories/{categoryId}/sizes")]
    public IActionResult GetSizes(int categoryId)
    {
      var category = _db.GetCategoryById(categoryId);

      var sizes = _db.GetCategorySizes(categoryId);

      return new OkResponseResult($"Sizes for {JsonConvert.SerializeObject(category)}", new { Counts = sizes.Count, Sizes = sizes });
    }

    [HttpGet("market/categories/{categoryId}/products")]
    public IActionResult SelectProduct(int categoryId, [FromQuery]ProductsFilterViewModel filter)
    {
      var category = _db.GetCategoryById(categoryId);

      var (counts, list) =
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

      return new OkResponseResult($"Products for {JsonConvert.SerializeObject(category)}.", new { AllCounts = counts, Counts = list.Count, Products = list });
    }

    [HttpGet("monitoring/click/category")]
    public IActionResult IncrementCategoryClickCount([FromQuery] int categoryId)
    {
      _db.IncrementClickCategory(categoryId);
      return new OkResponseResult();
    }

    [HttpGet("monitoring/click/product")]
    public IActionResult IncrementProductClicksCount([FromQuery] int productId)
    {
      _db.IncrementClickProduct(productId);
      return new OkResponseResult();
    }
  }
}
