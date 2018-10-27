using System.Collections.Generic;
using System.Linq;
using ZebraData.Entities;

namespace ZebraData.Repositories
{
  public class ProductRepository
  {
    private readonly ZebraMainContext _db;

    public ProductRepository(ZebraMainContext db)
    {
      _db = db;
    }

    public List<ProductEntity> GetProduct()
    {
      return _db.ProductEntities.ToList();
    }
  }
}