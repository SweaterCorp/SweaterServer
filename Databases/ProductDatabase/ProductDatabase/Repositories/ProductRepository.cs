using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductDatabase.DTOs;
using ProductDatabase.Entities;

namespace ProductDatabase.Repositories
{
  public class ProductRepository
  {
    private  ProductContext Db { get; }

    public ProductRepository(ProductContext db)
    {
      Db = db;
    }

    public List<CategoryEntity> GetCategories()
    {
      return Db.CategoryEntities.ToList();
    }

    public List<SizeTypeEntity> GetCategorySizes(int categoryId)
    {
      return Db.SizeTypeEntities.FromSql(
          $"SELECT * FROM [dbo].[ufnGetCategorySizes]({categoryId})").ToList();

    }
    public  List<int> GetCategoryColors(int categoryId)
    {
      return   Db.ProductEntities.Where(x=>x.CategoryId == categoryId).Select(x=>x.ColorId).Distinct().ToList();

    }

    public List<BrandEntity> GetCategoryBrands(int categoryId)
    {
      return Db.BrandEntities.FromSql(
        $"SELECT * FROM [dbo].[ufnGetCategoryBrands]({categoryId})").ToList();

    }

    public void IncrementClickCategory(int categoryId)
    {
      var category = Db.CategoryEntities.First(x => x.CategoryId == categoryId);
      category.ClicksCount++;
      Db.SaveChanges();
    }

    public void IncrementClickProduct(int productId)
    {
      var product = Db.ProductEntities.First(x => x.ProductId == productId);
      product.ClicksCount++;
      Db.SaveChanges();
    }

    // TODO
    // Дублирование переписать потом
    public (int counts, List<ProductCardDto> list) SelectProducts(ProductsFilterDto filter, int offset, int count)
    {
      int allCounts = 0;
      IQueryable<ProductEntity> products;

      if (filter.BrandsIds.Any())
      {
        products = Db.ProductEntities.Where(x => x.CategoryId == filter.CategoryId && x.Price >= filter.MinimalPrice &&
                                                  x.Price <= filter.MaximalPrice &&
                                                  filter.BrandsIds.Contains(x.BrandId));
      }
      else
      {
        products = Db.ProductEntities.Where(x => x.CategoryId == filter.CategoryId && x.Price >= filter.MinimalPrice &&
                                                  x.Price <= filter.MaximalPrice);
      }



      var resultQuery = (from product in products
                                     join brand in Db.BrandEntities on product.BrandId equals brand.BrandId
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
          productCardDto.Color = colors.FirstOrDefault(x => x.ProductId == productCardDto.Product.ProductId && filter.ColorsIds.Contains(x.ColorId))?.ColorId ?? 0;
        }
      }
      else
      {
        foreach (var productCardDto in result)
        {
          productCardDto.Color = colors.FirstOrDefault(x => x.ProductId == productCardDto.Product.ProductId)?.ColorId ?? 0;
        }
      }

      return (allCounts, result.Where(x => x.Sizes.Any() && x.Color != 0 && x.Brand != null && x.Product != null).ToList());
    }


    private List<ProductSize> GetProductsSizes(ICollection<int> productsIds)
    {
      return Db.ProductSizeTypeEntities.Where(productSize => productsIds.Contains(productSize.ProductId)).GroupJoin(
        Db.SizeTypeEntities, productSize => productSize.SizeTypeId, size => size.SizeTypeId,
        (productSize, sizes) => new ProductSize { ProductId = productSize.ProductId, Sizes = sizes.Select(x => x) }).ToList();
    }

    private List<ProductColor> GetProductsColors(ICollection<int> productsIds)
    {
      return Db.ProductEntities.Where(productColor => productsIds.Contains(productColor.ProductId)).Select(x=> new ProductColor{ ProductId = x.ProductId, ColorId = x.ColorId}).ToList();
    }

    private class ProductColor
    {
      public int ProductId { get; set; }
      public int ColorId { get; set; }
    }

    private class ProductSize
    {
      public int ProductId { get; set; }
      public IEnumerable<SizeTypeEntity> Sizes { get; set; }
    }
  }
}