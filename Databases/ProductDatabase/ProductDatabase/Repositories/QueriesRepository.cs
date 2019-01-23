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

    public async Task IncrementClickCategoryAsync(int categoryId)
    {
      var category = Db.CategoryEntities.First(x => x.CategoryId == categoryId);
      category.ClicksCount++;
      await Db.SaveChangesAsync();
    }

    public async Task IncrementClickProductAsync(int productId)
    {
      var product = Db.ProductEntities.First(x => x.ProductId == productId);
      product.ClicksCount++;
      await Db.SaveChangesAsync();
    }

    public async Task<(int counts, List<ProductCardDto> list)> SelectProductsAsync(ProductsFilterDto filter, int offset,
      int count)
    {
      var allCounts = 0;
      var products = Db.ProductEntities.Where(x => x.CategoryId == filter.CategoryId &&
                                                   x.Price >= filter.MinimalPrice && x.Price <= filter.MaximalPrice);

      if (filter.BrandIds.Any()) products = products.Where(x => filter.BrandIds.Contains(x.BrandId));

      var resultQuery = from product in products
        join productColorGoodness in
        Db.ProductColorGoodnessEntities.Where(x => x.PersonalColorTypeId == filter.PersonalColorType.Id) on
        product.ProductId equals productColorGoodness.ProductId
        join brand in Db.BrandEntities on product.BrandId equals brand.BrandId
        orderby productColorGoodness.Goodness descending
        select new ProductCardDto {Product = product, Goodness = productColorGoodness.Goodness, Brand = brand};

      var result = await resultQuery.ToListAsync();

      allCounts = result.Count;

      var ids = result.Select(x => x.Product.ProductId).ToList();
      var sizes = await GetProductsSizes(ids);

      var wherePredicate = filter.SizeIds.Any()
        ? (x => filter.SizeIds.Contains(x.SizeTypeId))
        : (Func<SizeTypeEntity, bool>) (x => true);

      foreach (var productCardDto in result)
        productCardDto.Sizes = sizes.First(x => x.ProductId == productCardDto.Product.ProductId).Sizes
          .Where(wherePredicate).ToList();

      return (allCounts, result.Skip(offset).Take(count).Where(x => x.Sizes.Any()).ToList());
    }

    

    private async Task<List<ProductSize>> GetProductsSizes(ICollection<int> productsIds)
    {
      return await Db.ProductSizeTypeEntities.Where(productSize => productsIds.Contains(productSize.ProductId))
        .GroupJoin(Db.SizeTypeEntities, productSize => productSize.SizeTypeId, size => size.SizeTypeId,
          (productSize, sizes) => new ProductSize {ProductId = productSize.ProductId, Sizes = sizes.Select(x => x)})
        .ToListAsync();
    }

    private class ProductSize
    {
      public int ProductId { get; set; }
      public IEnumerable<SizeTypeEntity> Sizes { get; set; }
    }
  }
}