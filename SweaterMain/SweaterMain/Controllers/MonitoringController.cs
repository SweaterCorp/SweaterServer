using System.Threading.Tasks;
using CommonLibraries.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductDatabase.Repositories;

namespace SweaterMain.Controllers
{
  [Produces("application/json")]
  [EnableCors("AllowAllOrigin")]
  [Route("api/monitoring")]
  [ApiController]
  public class MonitoringController : ControllerBase
  {
    private QueriesRepository Db { get; }
    private ILogger<MonitoringController> Logger { get; }

    public MonitoringController(ILogger<MonitoringController> logger, QueriesRepository db)
    {
      Logger.LogInformation($"{nameof(MonitoringController)}.ctr.Start");

      Db = db;
      Logger = logger;

      Logger.LogInformation($"{nameof(MonitoringController)}.ctr.End");
    }

    [HttpPatch("click/category")]
    public async Task<IActionResult> IncrementCategoryClickCount([FromQuery] int categoryId)
    {
      Logger.LogInformation($"{nameof(MonitoringController)}.{nameof(IncrementCategoryClickCount)}.Start");

      await Db.IncrementClickCategoryAsync(categoryId);
      var result = new NoContentResponseResult($"Click was added to category {categoryId}.");

      Logger.LogInformation($"{nameof(MonitoringController)}.{nameof(IncrementCategoryClickCount)}.End");
      return result;
    }

    [HttpPatch("click/product")]
    public async Task<IActionResult> IncrementProductClicksCount([FromQuery] int productId)
    {
      Logger.LogInformation($"{nameof(MonitoringController)}.{nameof(IncrementProductClicksCount)}.Start");

      await Db.IncrementClickProductAsync(productId);
      var result = new NoContentResponseResult($"Click was added to product {productId}.");

      Logger.LogInformation($"{nameof(MonitoringController)}.{nameof(IncrementProductClicksCount)}.End");
      return result;
    }
  }
}