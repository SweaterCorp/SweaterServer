using System.Threading.Tasks;
using CommonLibraries.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductDatabase.Repositories;

namespace SweaterServer.Controllers
{
  /// <summary>
  ///   Monitoring users behaviors.
  /// </summary>
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
    ///   Increment category click.
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns>Message if everything OK</returns>
    /// <response code="201">Returns message, that everything is OK</response>
    [HttpPatch("click/category")]
    [ProducesResponseType(typeof(ResponseObject), 200)]
    [ProducesResponseType(typeof(ResponseObject), 400)]
    public async Task<IActionResult> IncrementCategoryClick([FromQuery] int categoryId)
    {
      Logger.LogInformation($"{nameof(MonitoringController)}.{nameof(IncrementCategoryClick)}.Start");

      await Db.IncrementClickCategoryAsync(categoryId);
      var result = new NoContentResponseResult($"Click was added to category {categoryId}.");

      Logger.LogInformation($"{nameof(MonitoringController)}.{nameof(IncrementCategoryClick)}.End");
      return result;
    }

    /// <summary>
    ///   Increment category click.
    /// </summary>
    /// <param name="productId"></param>
    /// <returns>Message if everything OK</returns>
    /// <response code="201">Returns message, that everything is OK</response>
    [HttpPatch("click/product")]
    [ProducesResponseType(typeof(ResponseObject), 200)]
    [ProducesResponseType(typeof(ResponseObject), 400)]
    public async Task<IActionResult> IncrementProductClicks([FromQuery] int productId)
    {
      Logger.LogInformation($"{nameof(MonitoringController)}.{nameof(IncrementProductClicks)}.Start");

      await Db.IncrementClickProductAsync(productId);
      var result = new NoContentResponseResult($"Click was added to product {productId}.");

      Logger.LogInformation($"{nameof(MonitoringController)}.{nameof(IncrementProductClicks)}.End");
      return result;
    }
  }
}