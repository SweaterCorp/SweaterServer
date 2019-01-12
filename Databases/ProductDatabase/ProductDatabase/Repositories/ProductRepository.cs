﻿using System;
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