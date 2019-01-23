using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibraries.CommonTypes;
using Microsoft.EntityFrameworkCore;
using ProductDatabase.DTOs;
using ProductDatabase.Entities;

namespace ProductDatabase.Repositories
{
  public class ProductColorGoodnessRepository
  {
    private ProductContext Db { get; }

    public ProductColorGoodnessRepository(ProductContext db)
    {
      Db = db;
    }

    public async Task FillProductColorGoodnessesFromProducts()
    {
      var productIds = await Db.ProductEntities
        .Select(x => x.ProductId)
        .ToListAsync();

      await AddProductGoodnesses(productIds);
    }

    private async Task AddProductGoodnesses(IEnumerable<int> productIds)
    {
      var newColors = new List<ProductColorGoodnessEntity>();
      foreach (var productId in productIds)
        if (!await Db.ColorGoodnessEntities.AnyAsync(x => x.ColorId == productId))
        {
          var autumn = new ProductColorGoodnessEntity { ProductId = productId, PersonalColorTypeId = PersonalColorType.Autumn.Id };
          var spring = new ProductColorGoodnessEntity { ProductId = productId, PersonalColorTypeId = PersonalColorType.Spring.Id };
          var summer = new ProductColorGoodnessEntity { ProductId = productId, PersonalColorTypeId = PersonalColorType.Summer.Id };
          var winter = new ProductColorGoodnessEntity { ProductId = productId, PersonalColorTypeId = PersonalColorType.Winter.Id };

          newColors.Add(autumn);
          newColors.Add(spring);
          newColors.Add(summer);
          newColors.Add(winter);
        }
      if (newColors.Count != 0)
      {
        Db.ProductColorGoodnessEntities.AddRange(newColors);
        await Db.SaveChangesAsync();
      }
    }

    public async Task<List<ProductColorGoodnessEntity>> GetProductColorGoodnesses()
    {
      return await Db.ProductColorGoodnessEntities.ToListAsync();
    }

    public async Task UpdateColorGoodnesses(IEnumerable<ProductColorGoodnessEntity> productColorGoodnesses)
    {
      //Db.Database.ExecuteSqlCommand("TRUNCATE TABLE [ColorMatching]");
      //await Db.ColorMatchingEntities.AddRangeAsync(colors);
      Db.ProductColorGoodnessEntities.UpdateRange(productColorGoodnesses);
      await Db.SaveChangesAsync();
    }

    public async Task<List<ProductWithColorGoodnessDto>> GetProductWithColorGoodnessAsync()
    {
      
      var products = await Db.ProductEntities.Select(x=> new {ProductId = x.ProductId, ColorIds = new List<int> { x.Color1Id, x.Color2Id, x.Color3Id, x.Color4Id, x.Color5Id, x.Color6Id, x.Color7Id}}).ToListAsync();

      var colors = await Db.ColorGoodnessEntities.ToListAsync();

     

      var result = new List<ProductWithColorGoodnessDto>();
      foreach (var product in products)
      {
        var productWithColor = new ProductWithColorGoodnessDto {ProductId = product.ProductId,   ProductColorGoodnesses = new List<ProductColorGoodnessDto>()};
        foreach (var colorId in product.ColorIds)
        {
          productWithColor.ProductColorGoodnesses.AddRange(colors.Where(x => x.ColorId == colorId)
            .Select(x => new ProductColorGoodnessDto
            {
              ColorId = colorId,
              PersonalColorTypeId = x.PersonalColorTypeId,
              Goodness = x.Goodness
            }));
        }
        result.Add(productWithColor);
      }

      return result;
    }
  }
}