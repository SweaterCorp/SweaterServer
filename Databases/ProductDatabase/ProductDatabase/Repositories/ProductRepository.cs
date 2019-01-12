using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
      var country = await GetOrCreateCountryAsync(productDto.Country);
      var brand = await GetOrCreateBrandAsync(productDto.BrandName);
      var category = await GetOrCreateCategoryAsync(productDto.CategoryTypeId);

      var product = new ProductEntity
      {
        BrandId = brand.BrandId,
        CategoryId = category.CategoryId,
        ClicksCount = 0,
        ColorId = productDto.ColorId,
        CreatedDate = DateTime.UtcNow,
        Description = "",
        IsAvailable = true,
        IsDeleted = false,
        Link = productDto.Link,
        MadeInCountryId = country.CountryId,
        PreviewPhotoId = 0,
        Price = productDto.Price,
        PrintTypeId = productDto.PrintTypeId,
        VendorCode = productDto.VendorCode
      };
      Db.ProductEntities.Add(product);
      await Db.SaveChangesAsync();

      var photos = await AddProductPhotos(product.ProductId, productDto.Photos);

      product.PreviewPhotoId = photos.FirstOrDefault()?.ProductId ?? 0;
      await Db.SaveChangesAsync();

      await AddProductSizes(product.ProductId, productDto.Sizes);
      return product;
    }

    public async Task<CountryEntity> GetOrCreateCountryAsync(string countryName)
    {
      var countryDb = await Db.CountryEntities.FirstOrDefaultAsync(x => x.RussianName == countryName);
      if (countryDb != null) return countryDb;

      countryDb = new CountryEntity {RussianName = countryName};
      Db.CountryEntities.Add(countryDb);
      await Db.SaveChangesAsync();
      return countryDb;
    }

    public async Task<CategoryEntity> GetOrCreateCategoryAsync(int categoryTypeId)
    {
      var categoryDb = await Db.CategoryEntities.FirstOrDefaultAsync(x => x.CategoryTypeId == categoryTypeId);
      if (categoryDb != null) return categoryDb;

      categoryDb = new CategoryEntity {CategoryTypeId = categoryTypeId};
      Db.CategoryEntities.Add(categoryDb);
      await Db.SaveChangesAsync();
      return categoryDb;
    }

    public async Task<BrandEntity> GetOrCreateBrandAsync(string brandName)
    {
      var brandDb = await Db.BrandEntities.FirstOrDefaultAsync(x => x.Name == brandName);
      if (brandDb != null) return brandDb;

      brandDb = new BrandEntity {Name = brandName};
      Db.BrandEntities.Add(brandDb);
      await Db.SaveChangesAsync();
      return brandDb;
    }

    //public async Task<ProductPhotoEntity> AddProductPhoto(int productId, string photoUrl)
    //{
    //  var product = await Db.ProductEntities.FirstOrDefaultAsync(x => x.ProductId == productId);
    //  if (product == null) throw new System.Exception("product is null");

    //  var photo = new ProductPhotoEntity {ProductId = productId, CreatedDate = DateTime.UtcNow, PhotoUrl = photoUrl};
    //  Db.ProductPhotoEntities.Add(photo);
    //  await Db.SaveChangesAsync();
    //  product.PreviewPhotoId = photo.ProductPhotoId;
    //  await Db.SaveChangesAsync();
    //  return photo;
    //}

    public async Task<List<ProductPhotoEntity>> AddProductPhotos(int productId, List<string> photoUrls)
    {
      var photos = photoUrls
        .Select(x => new ProductPhotoEntity {ProductId = productId, CreatedDate = DateTime.UtcNow, PhotoUrl = x})
        .ToList();
      Db.ProductPhotoEntities.AddRange(photos);
      await Db.SaveChangesAsync();
      return photos;
    }

    public async Task<List<ProductSizeTypeEntity>> AddProductSizes(int productId, List<SizeDto> sizeDtos)
    {
      var currentSizes = new List<(bool isAvailable, SizeTypeEntity size)>();
      foreach (var sizeDto in sizeDtos) currentSizes.Add((sizeDto.IsAvailable, await GetOrCreateSizeAsync(sizeDto)));

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

    public async Task<SizeTypeEntity> GetOrCreateSizeAsync(SizeDto sizeDto)
    {
      var sizeDb =
        await Db.SizeTypeEntities.FirstOrDefaultAsync(x => sizeDto.RussianSize == x.RussianSize &&
                                                           sizeDto.OtherSize == x.OtherSize &&
                                                           sizeDto.CountryCode == x.CountryCode);
      if (sizeDb != null) return sizeDb;

      sizeDb = new SizeTypeEntity
      {
        CountryCode = sizeDto.CountryCode,
        OtherSize = sizeDto.OtherSize,
        RussianSize = sizeDto.RussianSize
      };
      Db.SizeTypeEntities.Add(sizeDb);
      await Db.SaveChangesAsync();
      return sizeDb;
    }
  }
}