using CommonLibraries.Response;
using Microsoft.AspNetCore.Mvc;
using ZebraData.Repositories;

namespace ZebraMain.Controllers
{
  [Route("api/monitoring")]
  [ApiController]
  public class MonitoringController : ControllerBase
  {
    private readonly ProductRepository _db;

    public MonitoringController(ProductRepository db)
    {
      _db = db;
    }

    [HttpGet("click/category")]
    public IActionResult IncrementCategoryClickCount([FromQuery] int categoryId)
    {
      _db.IncrementClickCategory(categoryId);
      return new OkResponseResult("Click was added.");
    }

    [HttpGet("click/product")]
    public IActionResult IncrementProductClicksCount([FromQuery] int productId)
    {
      _db.IncrementClickProduct(productId);
      return new OkResponseResult("Click was added.");
    }
  }
}