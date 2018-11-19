using Microsoft.EntityFrameworkCore;
using ZebraData.Entities;
using ZebraData.Entities.ProductGroup;

namespace ZebraData
{
  public class ZebraMainContext : DbContext
  {
    public virtual DbSet<BrandEntity> BrandEntities { get; set; }
    public virtual DbSet<CategoryEntity> CategoryEntities { get; set; }
    public virtual DbSet<ColorTypeEntity> ColorTypeEntities { get; set; }
    public virtual DbSet<ProductEntity> ProductEntities { get; set; }
    public virtual DbSet<ProductPhotoEntity> ProductPhotoEntities { get; set; }
    public virtual DbSet<ProductSizeTypeEntity> ProductSizeTypeEntities { get; set; }
    public virtual DbSet<ProductColorTypeEntity> ProductColorTypeEntities { get; set; }
    public virtual DbSet<SizeTypeEntity> SizeTypeEntities { get; set; }
    public virtual DbSet<PrintTypeEntity>  PrintTypeEntities { get; set; }

    public ZebraMainContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<ProductSizeTypeEntity>().HasKey(x => new {x.ProductId, x.SizeTypeId});
      modelBuilder.Entity<ProductColorTypeEntity>().HasKey(x => new { x.ProductId, x.ColorTypeId });
    }
  }
}