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


    public CategoryEntity GetCategoryById(int categoryId)
    {
      return _db.CategoryEntities.FirstOrDefault(x => x.CategoryId == categoryId);
    }

    public List<CategoryEntity> GetCategories()
    {
      return _db.CategoryEntities.ToList();
    }

    public List<SizeTypeEntity> GetCategorySizes(int categoryId)
    {
      return _db.SizeTypeEntities.FromSql(
          $"SELECT * FROM [dbo].[ufnGetCategorySizes]({categoryId})").ToList();

    }
    public List<ColorTypeEntity> GetCategoryColors(int categoryId)
    {
      return _db.ColorTypeEntities.FromSql(
        $"SELECT * FROM [dbo].[ufnGetCategoryColors]({categoryId})").ToList();

    }

    public List<BrandEntity> GetCategoryBrands(int categoryId)
    {
      return _db.BrandEntities.FromSql(
        $"SELECT * FROM [dbo].[ufnGetCategoryBrands]({categoryId})").ToList();

    }

    public void IncrementClickCategory(int categoryId)
    {
      var category = _db.CategoryEntities.First(x => x.CategoryId == categoryId);
      category.ClicksCount++;
      _db.SaveChanges();
    }

    public void IncrementClickProduct(int productId)
    {
      var product = _db.ProductEntities.First(x => x.ProductId == productId);
      product.ClicksCount++;
      _db.SaveChanges();
    }

    // TODO
    // Дублирование переписать потом
    public (int counts, List<ProductCardDto> list) SelectProducts(ProductsFilterDto filter, int offset, int count)
    {
      int allCounts = 0;
      IQueryable<ProductEntity> products;

      if (filter.BrandsIds.Any())
      {
        products = _db.ProductEntities.Where(x => x.CategoryId == filter.CategoryId && x.Price >= filter.MinimalPrice &&
                                                  x.Price <= filter.MaximalPrice &&
                                                  filter.BrandsIds.Contains(x.BrandId));
      }
      else
      {
        products = _db.ProductEntities.Where(x => x.CategoryId == filter.CategoryId && x.Price >= filter.MinimalPrice &&
                                                  x.Price <= filter.MaximalPrice);
      }



      var resultQuery = (from product in products
                                     join brand in _db.BrandEntities on product.BrandId equals brand.BrandId
                                     select new ProductCardDto { Product = product, Brand = brand });
      allCounts = resultQuery.Count();
      var result = resultQuery.Skip(offset).Take(count).ToList();
      var ids = result.Select(x => x.Product.ProductId).ToList();
      var sizes = GetProductsSizes(ids);
      var colors = GetProductsColors(ids);

      if (filter.SizesIds.Any())
      {
        foreach (var productCardDto in result)
        {
          productCardDto.Sizes = sizes.FirstOrDefault(x => x.ProductId == productCardDto.Product.ProductId)?.Sizes
                                   .Where(x => filter.SizesIds.Contains(x.SizeTypeId)).ToList() ??
                                 new List<SizeTypeEntity>();
        }
      }
      else
      {
        foreach (var productCardDto in result)
        {
          productCardDto.Sizes = sizes.FirstOrDefault(x => x.ProductId == productCardDto.Product.ProductId)?.Sizes.ToList() ??
                                 new List<SizeTypeEntity>();
        }
      }

      if (filter.ColorsIds.Any())
      {

        foreach (var productCardDto in result)
        {
          productCardDto.Colors = colors.FirstOrDefault(x => x.ProductId == productCardDto.Product.ProductId)?.Colors
                                    .Where(x => filter.ColorsIds.Contains(x.ColorTypeId)).ToList() ??
                                  new List<ColorTypeEntity>();
        }
      }
      else
      {
        foreach (var productCardDto in result)
        {
          productCardDto.Colors = colors.FirstOrDefault(x => x.ProductId == productCardDto.Product.ProductId)?.Colors.ToList() ??
                                  new List<ColorTypeEntity>();
        }
      }

      return (allCounts, result.Where(x => x.Sizes.Any() && x.Colors.Any() && x.Brand != null && x.Product != null).ToList());
    }


    private List<ProductSize> GetProductsSizes(ICollection<int> productsIds)
    {
      return _db.ProductSizeTypeEntities.Where(productSize => productsIds.Contains(productSize.ProductId)).GroupJoin(
        _db.SizeTypeEntities, productSize => productSize.SizeTypeId, size => size.SizeTypeId,
        (productSize, sizes) => new ProductSize { ProductId = productSize.ProductId, Sizes = sizes.Select(x => x) }).ToList();
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