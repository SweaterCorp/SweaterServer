using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ZebraData.Entities.ProductGroup;

namespace ZebraData.Repositories
{
  public class ProductRepository
  {
    private readonly ZebraMainContext _db;

    public ProductRepository(ZebraMainContext db)
    {
      _db = db;
    }

    public List<SizeTypeEntity> GetAllSizes(int categoryId)
    {
      return _db.SizeTypeEntities.FromSql(
          $"SELECT * FROM [dbo].[ufnGetSizes]({categoryId})").ToList();

    }

    public List<CategoryEntity> GetCategories()
    {
      return _db.CategoryEntities.ToList();
    }

    public void IncrementClickCategory(int categoryId)
    {
      CategoryEntity category = _db.CategoryEntities.First(x => x.CategoryId == categoryId);
      category.ClickCounts++;
      _db.SaveChanges();
    }

    public void IncrementClickProduct(int productId)
    {
      ProductEntity product = _db.ProductEntities.First(x => x.ProductId == productId);
      product.ClickCounts++;
      _db.SaveChanges();
    }

    public List<ProductCardDto> SelectProducts(int categoryId, decimal minimalPrice, decimal maximalPrice, List<int> sizesIds, int offset, int count)
    {

      var result = (from product in _db.ProductEntities.Where(x => x.CategoryId == categoryId && x.Price >= minimalPrice && x.Price <= maximalPrice)
                                                 join brand in _db.BrandEntities on product.BrandId equals brand.BrandId
                                                 select new ProductCardDto { Product = product, Brand = brand }).Skip(offset).Take(count).ToList();
      var ids = result.Select(x => x.Product.ProductId).ToList();
      var sizes = GetProductsSizes(ids);
      var colors = GetProductsColors(ids);

      foreach (var productCardDto in result)
      {
        productCardDto.Sizes = sizes.FirstOrDefault(x => x.ProductId == productCardDto.Product.ProductId)?.Sizes.Where(x=>sizesIds.Contains(x.SizeTypeId)).ToList() ??
                               new List<SizeTypeEntity>();
      }

      foreach (var productCardDto in result)
      {
        productCardDto.Colors = colors.FirstOrDefault(x => x.ProductId == productCardDto.Product.ProductId)?.Colors.ToList() ??
                               new List<ColorTypeEntity>();
      }

      return result.Where(x=>x.Sizes.Any()).ToList();
    }


    private List<ProductSize> GetProductsSizes(ICollection<int> productsIds)
    {
      return _db.ProductSizeTypeEntities.Where(productSize => productsIds.Contains(productSize.ProductId)).GroupJoin(
        _db.SizeTypeEntities, productSize => productSize.SizeTypeId, size => size.SizeTypeId,
        (productSize, sizes) => new ProductSize {ProductId = productSize.ProductId, Sizes = sizes.Select(x => x)}).ToList();
    }

    private List<ProductColors> GetProductsColors(ICollection<int> productsIds)
    {
      return _db.ProductColorTypeEntities.Where(productColor => productsIds.Contains(productColor.ProductId)).GroupJoin(
        _db.ColorTypeEntities, productColor => productColor.ColorTypeId, color => color.ColorTypeId,
        (productSize, colors) => new ProductColors { ProductId = productSize.ProductId, Colors = colors.Select(x => x) }).ToList();
    }

    private class ProductColors
    {
      public int ProductId { get; set; }
      public IEnumerable<ColorTypeEntity> Colors { get; set; }
    }

    private class ProductSize
    {
      public int ProductId { get; set; }
      public IEnumerable<SizeTypeEntity> Sizes { get; set; }
    }
  }
}