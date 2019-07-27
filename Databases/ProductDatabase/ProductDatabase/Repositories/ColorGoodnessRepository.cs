using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibraries.CommonTypes;
using Microsoft.EntityFrameworkCore;
using ProductDatabase.Entities;

namespace ProductDatabase.Repositories
{
  public class ColorGoodnessRepository
  {
    private ProductContext Db { get; }

    public ColorGoodnessRepository(ProductContext db)
    {
      Db = db;
    }

    public async Task FillColorGoodnessesFromProducts()
    {
      var colors = new List<int> { 0 };
      var dbColors = await Db.ProductEntities
        .Select(x => new List<int> { x.Color1Id, x.Color2Id, x.Color3Id, x.Color4Id, x.Color5Id, x.Color6Id, x.Color7Id })
        .ToListAsync();
      foreach (var dbColor in dbColors) colors.AddRange(dbColor);

      await AddColorGoodneses(colors.Distinct().Where(x => x >= 0).OrderBy(x => x).ToList());
    }

    private async Task AddColorGoodneses(IEnumerable<int> colorIds)
    {
      var newColors = new List<ColorGoodnessEntity>();
      foreach (var colorId in colorIds)
        if (!await Db.ColorGoodnessEntities.AnyAsync(x => x.ColorId == colorId))
        {
          var autumn = new ColorGoodnessEntity { ColorId = colorId, PersonalColorTypeId = (int)PersonalColorType.Autumn };
          var spring = new ColorGoodnessEntity { ColorId = colorId, PersonalColorTypeId = (int)PersonalColorType.Spring };
          var summer = new ColorGoodnessEntity { ColorId = colorId, PersonalColorTypeId = (int)PersonalColorType.Summer };
          var winter = new ColorGoodnessEntity { ColorId = colorId, PersonalColorTypeId = (int)PersonalColorType.Winter };

          newColors.Add(autumn);
          newColors.Add(spring);
          newColors.Add(summer);
          newColors.Add(winter);
        }
      if (newColors.Count != 0)
      {
        Db.ColorGoodnessEntities.AddRange(newColors);
        await Db.SaveChangesAsync();
      }
    }

    public async Task<List<ColorGoodnessEntity>> GetColorGoodnesses()
    {
      return await Db.ColorGoodnessEntities.ToListAsync();
    }

    public async Task UpdateColorGoodnesses(IEnumerable<ColorGoodnessEntity> colorGoodnesses)
    {
      //Db.Database.ExecuteSqlCommand("TRUNCATE TABLE [ColorMatching]");
      //await Db.ColorMatchingEntities.AddRangeAsync(colors);
      Db.ColorGoodnessEntities.UpdateRange(colorGoodnesses);
      await Db.SaveChangesAsync();
    }
  }
}