using System.Linq;
using System.Threading.Tasks;

namespace ProductDatabase.Repositories
{
  public class MonitoringRepository
  {
    private ProductContext Db { get; }

    public MonitoringRepository(ProductContext db)
    {
      Db = db;
    }

    public async Task<bool> IncrementClickCategoryAsync(int categoryId)
    {
      var category = Db.CategoryEntities.First(x => x.CategoryId == categoryId);
      category.ClicksCount++;
      return await Db.SaveChangesAsync() > 0;
    }

    public async Task<bool> IncrementClickProductAsync(int productId)
    {
      var product = Db.ProductEntities.First(x => x.ProductId == productId);
      product.ClicksCount++;
      return await Db.SaveChangesAsync() > 0;
    }
  }
}