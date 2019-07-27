using System.Linq;
using CommonLibraries.ColorAlgos;
using CommonLibraries.Infrastructures.ColorsData;
using CommonLibraries.Resources;
using CommonLibraries.Resources.DeserializerTypes;
using CommonLibraries.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductDatabase.Repositories;
using SweaterServer.ViewModels;

namespace SweaterServer.Controllers
{
  [Produces("application/json")]
  [EnableCors("AllowAllOrigin")]
  [Route("colors")]
  [ApiController]
  public class ColorController : ControllerBase
  {
    private QueriesRepository Db { get; }
    private ILogger<ColorController> Logger { get; }

    public ColorController(ILogger<ColorController> logger, QueriesRepository db)
    {
      Logger = logger;
      Db = db;
    }

    /// <summary>
    /// Get Personal Colors
    /// </summary>
    /// <returns></returns>
    [HttpGet("personal")]
    public IActionResult GetPersonalColors()
    {
      var collection = new PersonalColorCollection();
      var eyeColors = collection.Autumn.EyeColors.Union(collection.Spring.EyeColors).Union(collection.Summer.EyeColors)
        .Union(collection.Winter.EyeColors);
      var hairColors = collection.Autumn.EyeColors.Union(collection.Spring.HairColors)
        .Union(collection.Summer.HairColors).Union(collection.Winter.HairColors);
      var skinColors = collection.Autumn.EyeColors.Union(collection.Spring.SkinTones).Union(collection.Summer.SkinTones)
        .Union(collection.Winter.SkinTones);

      return new OkResponseResult(new { EyeColors = eyeColors, HairColors = hairColors, SkinTones = skinColors });
    }


    /// <summary>
    /// Get colors
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetColors()
    {
      Logger.LogInformation($"{nameof(ColorController)}.{nameof(GetColors)}.Start");

      var contentPath = new ContentPath();
      var handler = new ResourceHandler();
      var desColors = handler.ReadeResourceFile<ColorMatchingDeserializer>(contentPath.ColorsMatching);
      var colors = ColorMatching.FromColorsMatchingDeserializer(desColors);
      var result = new ColorCategoryViewModel
      {
        Blue =
          new ColorViewModel
          {
            Id = colors.Autumn.Blue.Id,
            Name = colors.Autumn.Blue.Name,
            Hexes = colors.Autumn.Blue.BaseColors.Select(x => x.Color.ToHex()).ToList()
          },
        BrownBeige =
          new ColorViewModel
          {
            Id = colors.Autumn.BrownBeige.Id,
            Name = colors.Autumn.BrownBeige.Name,
            Hexes = colors.Autumn.BrownBeige.BaseColors.Select(x => x.Color.ToHex()).ToList()
          },
        GrayBlackWhite =
          new ColorViewModel
          {
            Id = colors.Autumn.GrayBlackWhite.Id,
            Name = colors.Autumn.GrayBlackWhite.Name,
            Hexes = colors.Autumn.GrayBlackWhite.BaseColors.Select(x => x.Color.ToHex()).ToList()
          },
        Green =
          new ColorViewModel
          {
            Id = colors.Autumn.Green.Id,
            Name = colors.Autumn.Green.Name,
            Hexes = colors.Autumn.Green.BaseColors.Select(x => x.Color.ToHex()).ToList()
          },
        OrangeYellow =
          new ColorViewModel
          {
            Id = colors.Autumn.OrangeYellow.Id,
            Name = colors.Autumn.OrangeYellow.Name,
            Hexes = colors.Autumn.OrangeYellow.BaseColors.Select(x => x.Color.ToHex()).ToList()
          },
        Purple = new ColorViewModel
        {
          Id = colors.Autumn.Purple.Id,
          Name = colors.Autumn.Purple.Name,
          Hexes = colors.Autumn.Purple.BaseColors.Select(x => x.Color.ToHex()).ToList()
        },
        RedPink = new ColorViewModel
        {
          Id = colors.Autumn.RedPink.Id,
          Name = colors.Autumn.RedPink.Name,
          Hexes = colors.Autumn.RedPink.BaseColors.Select(x => x.Color.ToHex()).ToList()
        }
      };
      Logger.LogInformation($"{nameof(ColorController)}.{nameof(GetColors)}.Start");
      return new OkResponseResult("Colors:", new { Colors = result });
    }
  }
}