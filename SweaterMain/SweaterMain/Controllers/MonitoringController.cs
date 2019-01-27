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
    private MonitoringRepository Db { get; }
    private ILogger<MonitoringController> Logger { get; }

    public MonitoringController(ILogger<MonitoringController> logger, MonitoringRepository db)
    {
      Logger.LogInformation($"{nameof(MonitoringController)}.ctr.Start");

      Db = db;
      Logger = logger;

      Logger.LogInformation($"{nameof(MonitoringController)}.ctr.End");
    }

    /// <summary>
    /// Creates a TodoItem.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Todo
    ///     {
    ///        "id": 1,
    ///        "name": "Item1",
    ///        "isComplete": true
    ///     }
    ///
    /// </remarks>
    /// <param name="item"></param>
    /// <returns>A newly created TodoItem</returns>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>            
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
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