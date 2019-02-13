using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductDatabase.DTOs;
using ProductDatabase.Entities;

namespace ProductDatabase.Repositories
{
  public class QueriesRepository
  {
    private ProductContext Db { get; }

    public QueriesRepository(ProductContext db)
    {
      Db = db;
    }

    public async Task<List<CategoryEntity>> GetCategoriesAsync()
    {
      return await Db.CategoryEntities.ToListAsync();
    }

    public async Task<CategoryEntity> GetCategoryByIdAsync(int categoryId)
    {
      return await Db.CategoryEntities.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
    }

    public async Task<List<SizeTypeEntity>> GetCategorySizesAsync(int categoryId)
    {
      return await Db.SizeTypeEntities.FromSql($"SELECT * FROM [dbo].[ufnGetCategorySizes]({categoryId})")
        .ToListAsync();
    }

    public async Task<List<int>> GetCategoryColorsAsync(int categoryId)
    {
      return await Db.ProductEntities.Where(x => x.CategoryId == categoryId).Select(x => x.ShopColorId).Distinct()
        .ToListAsync();
    }

    public async Task<List<BrandEntity>> GetCategoryBrandsAsync(int categoryId)
    {
      return await Db.BrandEntities.FromSql($"SELECT * FROM [dbo].[ufnGetCategoryBrands]({categoryId})").ToListAsync();
    }

    
    public async Task<(int counts, List<ProductCardDto> list)> SelectProductsAsync(ProductFilterDto filter, int offset,
      int count)
    {
      var products = Db.ProductEntities.Where(x => x.CategoryId == filter.CategoryId && x.Price >= filter.MinimalPrice && x.Price <= filter.MaximalPrice);

      if (filter.BrandIds.Any()) products = products.Where(x => filter.BrandIds.Contains(x.BrandId));

      var productGoodnesses = Db.ProductColorGoodnessEntities.Where(x => x.PersonalColorTypeId == filter.PersonalColorType.Id);

      var resultQuery = from product in products
        join productColorGoodness in productGoodnesses on product.ProductId equals productColorGoodness.ProductId
        join brand in Db.BrandEntities on product.BrandId equals brand.BrandId
        join photo in Db.ProductPhotoEntities on product.ProductId equals photo.ProductId
        orderby productColorGoodness.Goodness descending

        select new {product, brand, productColorGoodness, photo} into selectResult
        group selectResult by selectResult.product into groupByProduct
        select new ProductCardDto
        {
          Product = groupByProduct.Key,
          Goodness = groupByProduct.First(x=>x.product.ProductId == groupByProduct.Key.ProductId).productColorGoodness.Goodness,
          Brand = groupByProduct.First(x => x.product.ProductId == groupByProduct.Key.ProductId).brand,
          ProductPhotos = groupByProduct.Where(x => x.product.ProductId == groupByProduct.Key.ProductId).Select(x=>x.photo)
        };

      var result = await resultQuery.ToListAsync();

      var allCounts = result.Count;

      var ids = result.Select(x => x.Product.ProductId).ToList();
      var sizes = await GetProductsSizes(ids);

      var sizePredicate = filter.SizeIds.Any() ? (x => filter.SizeIds.Contains(x.SizeTypeId)) : (Func<SizeTypeEntity, bool>) (x => true);

      foreach (var productCardDto in result)
        productCardDto.Sizes = sizes.First(x => x.ProductId == productCardDto.Product.ProductId).Sizes
          .Where(sizePredicate).ToList();

      return (allCounts, result.Skip(offset).Take(count).Where(x => x.Sizes.Any()).ToList());
    }

    

    private async Task<List<ProductSize>> GetProductsSizes(ICollection<int> productsIds)
    {

      var result =
        from productSize in Db.ProductSizeTypeEntities.Where(productSize => productsIds.Contains(productSize.ProductId))
        join size in Db.SizeTypeEntities on productSize.SizeTypeId equals size.SizeTypeId
        select new {productSize, size}
        into selectResult
        group selectResult by selectResult.productSize.ProductId
        into g
        select new ProductSize
        {
          ProductId = g.Key,
          Sizes = g.Where(x => x.productSize.ProductId == g.Key).Select(x => x.size)
        };

      return await result.ToListAsync();
    }

    private class ProductSize
    {
      public int ProductId { get; set; }
      public IEnumerable<SizeTypeEntity> Sizes { get; set; }
    }
  }
}