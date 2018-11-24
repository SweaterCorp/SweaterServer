using System;
using System.Collections.Generic;
using System.Net;
using CommonLibraries.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    private readonly ProductRepository _db;
    private readonly IHostingEnvironment _appEnvironment;

    public GeneralController(IHostingEnvironment appEnvironment, ProductRepository db)
    {
      _db = db;
      _appEnvironment = appEnvironment;
    }

    [HttpGet("market/categories")]
    public IActionResult GetCategories()
    {
      var categories = _db.GetCategories();
      return new OkResponseResult("Categories", new {Counts = categories.Count, Categories = categories});
    }

    [HttpGet("market/categories/{categoryId}/brands")]
    public IActionResult GetBrands(int categoryId)
    {
      var category = _db.GetCategoryById(categoryId);

      var brands = _db.GetCategoryBrands(categoryId);

      return new OkResponseResult($"Brands for category:{category.CategoryId} {category.RussianName}.",
        new CategoryContainerViewModel<BrandEntity>(category, brands.Count, brands));
    }

    [HttpGet("market/categories/{categoryId}/colors")]
    public IActionResult GetColors(int categoryId)
    {
      var category = _db.GetCategoryById(categoryId);

      var colors = _db.GetCategoryColors(categoryId);

      return new OkResponseResult($"Colors for category:{category.CategoryId} {category.RussianName}.",
        new CategoryContainerViewModel<ColorTypeEntity>(category, colors.Count, colors));
    }

    [HttpGet("market/categories/{categoryId}/sizes")]
    public IActionResult GetSizes(int categoryId)
    {
      var category = _db.GetCategoryById(categoryId);

      var sizes = _db.GetCategorySizes(categoryId);

      return new OkResponseResult($"Sizes for category:{category.CategoryId} {category.RussianName}.",
        new CategoryContainerViewModel<SizeTypeEntity>(category, sizes.Count, sizes));
    }

    [HttpGet("market/categories/{categoryId}/products")]
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

    [HttpPost("media/photo/file/upload")]
    public IActionResult UploadUserPhotoViaFile(UploadPhotoViewModel uploadPhoto)
    {
      
      return new OkResponseResult($"hello", _appEnvironment);
    }

    [HttpGet("media/photo/url/upload")]
    public IActionResult UploadUserPhotoViaUrl([FromQuery] int userId, [FromQuery] string url)
    {
      return new OkResponseResult($"hello", _appEnvironment);
      //if (!ModelState.IsValid) return new BadResponseResult(ModelState);

      //if (MediaService.IsAlreadyDownloaded(background.Url))
      //  return new OkResponseResult(new UrlViewModel { Url = new Uri(MediaConverter.ToFullBackgroundurlUrl(background.Url, BackgroundSizeType.Original)).LocalPath });


      //var url = MediaService.UploadBackground(background.Url, background.BackgroundType)
      //  .FirstOrDefault(x => x.Size == BackgroundSizeType.Original)?.Url;
      //return url.IsNullOrEmpty()
      //  ? new ResponseResult((int)HttpStatusCode.NotModified)
      //  : new OkResponseResult(new UrlViewModel { Url = url });
    }

    [HttpGet("monitoring/click/category")]
    public IActionResult IncrementCategoryClickCount([FromQuery] int categoryId)
    {
      _db.IncrementClickCategory(categoryId);
      return new OkResponseResult("Click was added.");
    }

    [HttpGet("monitoring/click/product")]
    public IActionResult IncrementProductClicksCount([FromQuery] int productId)
    {
      _db.IncrementClickProduct(productId);
      return new OkResponseResult("Click was added.");
    }
  }
}