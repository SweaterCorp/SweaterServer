using CommonLibraries.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SweaterData.Repositories;

namespace SweaterMain.Controllers
{
  [Produces("application/json")]
  [EnableCors("AllowAllOrigin")]
  [Route("api/monitoring")]
  [ApiController]
  public class MonitoringController : ControllerBase
  {
    private ProductRepository Db { get; }
    private ILogger<UserController> Logger { get; }

    public MonitoringController(ILogger<UserController> logger, ProductRepository db)
    {
      Logger.LogInformation($"{nameof(MonitoringController)}.ctr.Start");

      Db = db;
      Logger = logger;

      Logger.LogInformation($"{nameof(MonitoringController)}.ctr.End");
    }

    [HttpPatch("click/category")]
    public IActionResult IncrementCategoryClickCount([FromQuery] int categoryId)
    {
      Logger.LogInformation($"{nameof(MonitoringController)}.{nameof(IncrementCategoryClickCount)}.Start");

      Db.IncrementClickCategory(categoryId);
      var result = new NoContentResponseResult($"Click was added to category {categoryId}.");

      Logger.LogInformation($"{nameof(MonitoringController)}.{nameof(IncrementCategoryClickCount)}.End");
      return result;
    }

    [HttpPatch("click/product")]
    public IActionResult IncrementProductClicksCount([FromQuery] int productId)
    {
      Logger.LogInformation($"{nameof(MonitoringController)}.{nameof(IncrementProductClicksCount)}.Start");

      Db.IncrementClickProduct(productId);
      var result = new NoContentResponseResult($"Click was added to product {productId}.");

      Logger.LogInformation($"{nameof(MonitoringController)}.{nameof(IncrementProductClicksCount)}.End");
      return result;
    }
  }
}