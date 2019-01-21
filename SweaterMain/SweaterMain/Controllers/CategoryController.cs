using System.Collections.Generic;
using System.Linq;
using System.Net;
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
  public class CategoriesController : ControllerBase
  {
    private QueriesRepository Db { get; }
    private ColorRepository ColorDb { get; }
    private ILogger<CategoriesController> Logger { get; }

    public CategoriesController(ILogger<CategoriesController> logger, QueriesRepository db, ColorRepository colorDb)
    {
      Logger = logger;
      Db = db;
      ColorDb = colorDb;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
      Logger.LogInformation($"{nameof(CategoriesController)}.{nameof(GetCategories)}.Start");

      var categories = await Db.GetCategoriesAsync();
      var result =  new OkResponseResult("Categories", new {Counts = categories.Count, Categories = categories});

      Logger.LogInformation($"{nameof(CategoriesController)}.{nameof(GetCategories)}.End");
      return result;
    }

    [HttpGet("{categoryId}/brands")]
    public async Task<IActionResult> GetBrands(int categoryId)
    {
      Logger.LogInformation($"{nameof(CategoriesController)}.{nameof(GetBrands)}.Start");

      var category = await  Db.GetCategoryByIdAsync(categoryId);
      if (category == null) return new ResponseResult(HttpStatusCode.NotFound, $"There is no category with id:{categoryId}");

      var brands = await Db.GetCategoryBrandsAsync(categoryId);

      var result =  new OkResponseResult($"Brands for category:{category.CategoryId} {((CategoryType)category.CategoryTypeId).Name}.",
        new CategoryContainerViewModel<BrandEntity>(category, brands.Count, brands));

      Logger.LogInformation($"{nameof(CategoriesController)}.{nameof(GetBrands)}.End");
      return result;
    }

    [HttpGet("{categoryId}/sizes")]
    public async Task<IActionResult> GetSizes(int categoryId)
    {
      Logger.LogInformation($"{nameof(CategoriesController)}.{nameof(GetSizes)}.Start");

      var category = await Db.GetCategoryByIdAsync(categoryId);
      if (category == null) return new NotFoundResponseResult($"There is no category with id:{categoryId}");

      var sizes = await Db.GetCategorySizesAsync(categoryId);

      var result =  new OkResponseResult($"Sizes for category:{category.CategoryId} {((CategoryType)category.CategoryTypeId).Name}.",
        new CategoryContainerViewModel<SizeTypeEntity>(category, sizes.Count, sizes));

      Logger.LogInformation($"{nameof(CategoriesController)}.{nameof(GetSizes)}.Start");
      return result;
    }


    [HttpGet("{categoryId}/products")]
    public async Task<IActionResult> SelectProduct(int categoryId, [FromQuery] ProductsFilterViewModel filter)
    {
      Logger.LogInformation($"{nameof(CategoriesController)}.{nameof(SelectProduct)}.Start");

      var category = await Db.GetCategoryByIdAsync(categoryId);
      if (category == null) return new NotFoundResponseResult($"There is no category with id:{categoryId}");

      var colors = await ColorDb.GetColorGoodness((PersonalColorType)filter.PersonalColorTypeId, (ColorGroupType)filter.ColorCategoryId);

      (int counts, List<ProductCardDto> list) = await
        Db.SelectProductsAsync(
          new ProductsFilterDto
          {
            CategoryId = categoryId,
            MinimalPrice = filter.MinimalPrice,
            MaximalPrice = filter.MaximalPrice,
            BrandsIds = filter.BrandsIds,
            SizesIds = filter.SizesIds
          }, filter.PageParams.Offset, filter.PageParams.Count);

      for (int i = 0; i < list.Count; i++)
      {
        var product = list[i];
        product.Goodness = GetColorGoodness(product.Product.Color1Id);
        if (GetColorGoodness(product.Product.Color2Id) > product.Goodness) product.Goodness = GetColorGoodness(product.Product.Color2Id);
        if (GetColorGoodness(product.Product.Color3Id) > product.Goodness) product.Goodness = GetColorGoodness(product.Product.Color3Id);
        if (GetColorGoodness(product.Product.Color4Id) > product.Goodness) product.Goodness = GetColorGoodness(product.Product.Color4Id);
        if (GetColorGoodness(product.Product.Color5Id) > product.Goodness) product.Goodness = GetColorGoodness(product.Product.Color5Id);
        if (GetColorGoodness(product.Product.Color5Id) > product.Goodness) product.Goodness = GetColorGoodness(product.Product.Color6Id);
        if (GetColorGoodness(product.Product.Color6Id) > product.Goodness) product.Goodness = GetColorGoodness(product.Product.Color7Id);
        //product.Goodness = GetColorGoodness(product.Product.Color1Id) + GetColorGoodness(product.Product.Color2Id) +
        //                   GetColorGoodness(product.Product.Color3Id) + GetColorGoodness(product.Product.Color4Id) +
        //                   GetColorGoodness(product.Product.Color5Id) + GetColorGoodness(product.Product.Color6Id) +
        //                   GetColorGoodness(product.Product.Color7Id);
      }


      var result = new OkResponseResult($"Products for category:{category.CategoryId} {((CategoryType)category.CategoryTypeId).Name}.",
        new { Category = category, AllCounts = counts, Data = new { Counts = list.Count, Products = list.OrderByDescending(x => x.Goodness).Skip(filter.PageParams.Offset).Take(filter.PageParams.Count) } });

      Logger.LogInformation($"{nameof(CategoriesController)}.{nameof(SelectProduct)}.Start");
      return result;

      double GetColorGoodness(int colorId)
      {
        if (colorId == -1) return 0.0;
        return colors.Find(x => x.ColorId == colorId).Goodness;
      }
    }


    [HttpGet("{categoryId}/select_product_with_special_color")]
    public async Task<IActionResult> SelectProduct(int categoryId, [FromQuery] ProductsFilterWithColorViewModel filter)
    {
      Logger.LogInformation($"{nameof(CategoriesController)}.{nameof(SelectProduct)}.Start");

      var category = await Db.GetCategoryByIdAsync(categoryId);
      if (category == null) return new NotFoundResponseResult($"There is no category with id:{categoryId}");

      var colors = await ColorDb.GetColorGoodness((PersonalColorType)filter.PersonalColorTypeId, (ColorGroupType)filter.ColorCategoryId);

      (int counts, List<ProductCardDto> list) = await 
        Db.SelectProductsAsync(
          new ProductsFilterDto
          {
            CategoryId = categoryId,
            MinimalPrice = filter.MinimalPrice,
            MaximalPrice = filter.MaximalPrice,
            BrandsIds = filter.BrandsIds,
            SizesIds = filter.SizesIds
          }, filter.PageParams.Offset, filter.PageParams.Count);

      for (int i = 0; i < list.Count; i++)
      {
        var product = list[i];
        product.Goodness = GetColorGoodness(product.Product.Color1Id);
        if (GetColorGoodness(product.Product.Color2Id) > product.Goodness)product.Goodness = GetColorGoodness(product.Product.Color2Id);
        if (GetColorGoodness(product.Product.Color3Id) > product.Goodness)product.Goodness = GetColorGoodness(product.Product.Color3Id);
        if (GetColorGoodness(product.Product.Color4Id) > product.Goodness)product.Goodness = GetColorGoodness(product.Product.Color4Id);
        if (GetColorGoodness(product.Product.Color5Id) > product.Goodness)product.Goodness = GetColorGoodness(product.Product.Color5Id);
        if (GetColorGoodness(product.Product.Color5Id) > product.Goodness)product.Goodness = GetColorGoodness(product.Product.Color6Id);
        if (GetColorGoodness(product.Product.Color6Id) > product.Goodness)product.Goodness = GetColorGoodness(product.Product.Color7Id);
        //product.Goodness = GetColorGoodness(product.Product.Color1Id) + GetColorGoodness(product.Product.Color2Id) +
        //                   GetColorGoodness(product.Product.Color3Id) + GetColorGoodness(product.Product.Color4Id) +
        //                   GetColorGoodness(product.Product.Color5Id) + GetColorGoodness(product.Product.Color6Id) +
        //                   GetColorGoodness(product.Product.Color7Id);
      }


      var result = new OkResponseResult($"Products for category:{category.CategoryId} {((CategoryType)category.CategoryTypeId).Name}.",
        new { Category = category, AllCounts = counts, Data = new { Counts = list.Count, Products = list.OrderByDescending(x=>x.Goodness).Skip(filter.PageParams.Offset).Take(filter.PageParams.Count) } });

      Logger.LogInformation($"{nameof(CategoriesController)}.{nameof(SelectProduct)}.Start");
      return result;

      double GetColorGoodness(int colorId)
      {
        if (colorId == -1) return 0.0;
        return colors.Find(x => x.ColorId == colorId).Goodness;
      }
    }
  }
}