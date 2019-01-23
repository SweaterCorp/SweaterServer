using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibraries.CommonTypes;
using CommonLibraries.Extensions;
using Microsoft.EntityFrameworkCore;
using ProductDatabase.DTOs;
using ProductDatabase.Entities;

namespace ProductDatabase.Repositories
{
  public class GeneratedProductDataRepository
  {
    private ProductContext Db { get; }

    public GeneratedProductDataRepository(ProductContext db)
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

      await AddColorGoodnesses(productDto.ColorIds);

      return product;
    }

    // TODO логика от цветового репозитория - вынести наверх потом, в другой класс, который будет разруливать это
    private async Task AddColorGoodnesses(IEnumerable<int> colorIds)
    {
      var newColors = new List<ColorGoodnessEntity>();
      foreach (var colorId in colorIds)
        if (!await Db.ColorGoodnessEntities.AnyAsync(x => x.ColorId == colorId))
        {
          var autumn = new ColorGoodnessEntity { ColorId = colorId, PersonalColorTypeId = PersonalColorType.Autumn.Id };
          var spring = new ColorGoodnessEntity { ColorId = colorId, PersonalColorTypeId = PersonalColorType.Spring.Id };
          var summer = new ColorGoodnessEntity { ColorId = colorId, PersonalColorTypeId = PersonalColorType.Summer.Id };
          var winter = new ColorGoodnessEntity { ColorId = colorId, PersonalColorTypeId = PersonalColorType.Winter.Id };

          newColors.Add(autumn);
          newColors.Add(spring);
          newColors.Add(summer);
          newColors.Add(winter);
        }
      if (newColors.Count != 0)
      {
        Db.ColorGoodnessEntities.AddRange(newColors);
        await Db.SaveChangesAsync();
      }
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