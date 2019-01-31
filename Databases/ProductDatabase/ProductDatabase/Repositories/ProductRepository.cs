using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibraries.Extensions;
using Microsoft.EntityFrameworkCore;
using ProductDatabase.DTOs;
using ProductDatabase.Entities;

namespace ProductDatabase.Repositories
{
  public class ProductRepository
  {
    private ProductContext Db { get; }

    public ProductRepository(ProductContext db)
    {
      Db = db;
    }

    public async Task<ProductEntity> AddProduct(AddProductDto productDto)
    {
      if (await Db.ProductEntities.AnyAsync(x => x.VendorCode == productDto.VendorCode)) return null;

      var country = await GetOrCreateCountryAsync(productDto.Country);
      var brand = await GetOrCreateBrandAsync(productDto.BrandName);
      var category = await GetOrCreateCategoryAsync(productDto.CategoryTypeId);

      var color1Id = productDto.ColorIds.GetValueOrDefault(0, -1);
      var color2Id = productDto.ColorIds.GetValueOrDefault(1, -1);
      var color3Id = productDto.ColorIds.GetValueOrDefault(2, -1);
      var color4Id = productDto.ColorIds.GetValueOrDefault(3, -1);
      var color5Id = productDto.ColorIds.GetValueOrDefault(4, -1);
      var color6Id = productDto.ColorIds.GetValueOrDefault(5, -1);
      var color7Id = productDto.ColorIds.GetValueOrDefault(6, -1);

      var product = new ProductEntity
      {
        BrandId = brand.BrandId,
        PrintTypeId = productDto.PrintTypeId,
        ExtraPrintTypeId = productDto.ExtraPrintTypeId,
        CategoryId = category.CategoryId,
        ClicksCount = 0,
        ShopColorId = productDto.ShopColorId,
        ShopTypeId = productDto.ShopTypeId,
        Color1Id = color1Id,
        Color2Id = color2Id,
        Color3Id = color3Id,
        Color4Id = color4Id,
        Color5Id = color5Id,
        Color6Id = color6Id,
        Color7Id = color7Id,
        CreatedDate = DateTime.UtcNow,
        Description = "",
        IsAvailable = true,
        IsDeleted = false,
        Link = productDto.Link,
        MadeInCountryId = country.CountryId,
        PreviewPhotoId = 0,
        Price = productDto.Price,
        VendorCode = productDto.VendorCode
      };
      Db.ProductEntities.Add(product);
      await Db.SaveChangesAsync();

      var photos = await AddProductPhotos(product.ProductId, productDto.Photos);

      product.PreviewPhotoId = photos.FirstOrDefault()?.ProductPhotoId ?? 0;
      await Db.SaveChangesAsync();

      await AddProductSizes(product.ProductId, productDto.Sizes);

      return product;
    }

    private async Task<CountryEntity> GetOrCreateCountryAsync(string countryName)
    {
      var countryDb = await Db.CountryEntities.FirstOrDefaultAsync(x => x.RussianName == countryName);
      if (countryDb != null) return countryDb;

      countryDb = new CountryEntity {RussianName = countryName, EnglishName = string.Empty, FlagUrl = string.Empty};
      Db.CountryEntities.Add(countryDb);
      await Db.SaveChangesAsync();
      return countryDb;
    }

    private async Task<CategoryEntity> GetOrCreateCategoryAsync(int categoryTypeId)
    {
      var categoryDb = await Db.CategoryEntities.FirstOrDefaultAsync(x => x.CategoryTypeId == categoryTypeId);
      if (categoryDb != null) return categoryDb;

      categoryDb = new CategoryEntity {CategoryTypeId = categoryTypeId, CategoryPhotoUrl = string.Empty};
      Db.CategoryEntities.Add(categoryDb);
      await Db.SaveChangesAsync();
      return categoryDb;
    }

    private async Task<BrandEntity> GetOrCreateBrandAsync(string brandName)
    {
      var brandDb = await Db.BrandEntities.FirstOrDefaultAsync(x => x.Name == brandName);
      if (brandDb != null) return brandDb;

      brandDb = new BrandEntity {Name = brandName, Site = string.Empty, LogoUrl = string.Empty};
      Db.BrandEntities.Add(brandDb);
      await Db.SaveChangesAsync();
      return brandDb;
    }

    private async Task<List<ProductPhotoEntity>> AddProductPhotos(int productId, IEnumerable<string> photoUrls)
    {
      var photos = photoUrls
        .Select(x => new ProductPhotoEntity {ProductId = productId, CreatedDate = DateTime.UtcNow, PhotoUrl = x})
        .ToList();
      Db.ProductPhotoEntities.AddRange(photos);
      await Db.SaveChangesAsync();
      return photos;
    }

    private async Task<List<ProductSizeTypeEntity>> AddProductSizes(int productId, IEnumerable<SizeDto> sizeDtos)
    {
      var currentSizes = new List<(bool isAvailable, SizeTypeEntity size)>();
      foreach (var sizeDto in sizeDtos.Distinct(new SizeDto()))
        currentSizes.Add((sizeDto.IsAvailable, await GetOrCreateSizeAsync(sizeDto)));

      var productSizes = currentSizes.Select(sizeTypeEntity => new ProductSizeTypeEntity
      {
        IsAvailable = sizeTypeEntity.isAvailable,
        ProductId = productId,
        SizeTypeId = sizeTypeEntity.size.SizeTypeId,
        UpdatedDate = DateTime.UtcNow
      }).ToList();

      Db.ProductSizeTypeEntities.AddRange(productSizes);
      await Db.SaveChangesAsync();
      return productSizes;
    }

    private async Task<SizeTypeEntity> GetOrCreateSizeAsync(SizeDto sizeDto)
    {
      var sizeDb =
        await Db.SizeTypeEntities.FirstOrDefaultAsync(x => sizeDto.RussianSize == x.RussianSize &&
                                                           sizeDto.OtherCountry == x.OtherCountry &&
                                                           sizeDto.CountryCode == x.CountryCode);
      if (sizeDb != null) return sizeDb;

      sizeDb = new SizeTypeEntity
      {
        CountryCode = sizeDto.CountryCode,
        OtherCountry = sizeDto.OtherCountry,
        RussianSize = sizeDto.RussianSize
      };
      Db.SizeTypeEntities.Add(sizeDb);
      await Db.SaveChangesAsync();
      return sizeDb;
    }
  }
}