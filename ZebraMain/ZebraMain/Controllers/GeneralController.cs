using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibraries.Response;
using Microsoft.AspNetCore.Mvc;
using ZebraData;
using ZebraData.Entities;
using ZebraData.Entities.ProductGroup;
using ZebraData.Repositories;

namespace ZebraMain.Controllers
{
  [Route("api")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    ProductRepository _db;

    public ProductsController(ProductRepository db)
    {
      _db = db;
    }

    [HttpGet("categories")]
    public ActionResult<IEnumerable<CategoryEntity>> GetCategories()
    {
      return _db.CategoryEntities.ToList();
    }

    [HttpGet("sizes")]
    public ActionResult<IEnumerable<SizeTypeEntity>> GetSizes()
    {
      return _db.SizeTypeEntities.ToList();
    }


    [HttpGet("products/{id}")]
    public ProductEntity GetProduct(int id)
    {
      return _db.ProductEntities.FirstOrDefault(x => x.ProductId == id);
    }

    [HttpGet("products/select")]
    public ActionResult<IEnumerable<ProductEntity>> SelectProduct([FromQuery] string brand, [FromQuery] string color)
    {
      var colorId = _db.ColorTypeEntities.FirstOrDefault(x => x.RussianName == color)?.ColorTypeId ?? 0;
      var productIds = _db.ProductColorTypeEntities.Where(x => x.ColorTypeId == colorId).ToList();
      var t = _db.ProductColorTypeEntities.ToList();
      var brandId = _db.BrandEntities.FirstOrDefault(x => x.Name == brand)?.BrandId ?? 0;
      return _db.ProductEntities.Where(x => x.BrandId == brandId).Join(productIds, product => product.ProductId, productColor => productColor.ProductId, (first, second) => first).ToList();
    }

    [HttpGet("products/select")]
    public ActionResult<IEnumerable<ProductEntity>> SelectProduct([FromQuery] string brand, [FromQuery] string color)
    {
      var colorId = _db.ColorTypeEntities.FirstOrDefault(x => x.RussianName == color)?.ColorTypeId ?? 0;
      var productIds = _db.ProductColorTypeEntities.Where(x => x.ColorTypeId == colorId).ToList();
      var t = _db.ProductColorTypeEntities.ToList();
      var brandId = _db.BrandEntities.FirstOrDefault(x => x.Name == brand)?.BrandId ?? 0;
      return _db.ProductEntities.Where(x=>x.BrandId == brandId).Join(productIds, product => product.ProductId, productColor=>productColor.ProductId, (first, second)=>  first).ToList();
    }

    public ActionResult IncrementClickCategoryCount([FromQuery] int categoryId)
    {
      _db.IncrementClickCategory(categoryId);
      return new OkResponseResult();
    }
  }
}
