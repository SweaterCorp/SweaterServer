using Microsoft.EntityFrameworkCore;
using ZebraData.Entities;
using ZebraData.Entities.ProductGroup;

namespace ZebraData
{
  public class ZebraMainContext : DbContext
  {
    public virtual DbSet<BrandEntity> BrandEntities { get; set; }
    public virtual DbSet<CategoryEntity> CategoryEntities { get; set; }
    public virtual DbSet<ColorEntity> ColorEntities { get; set; }
    public virtual DbSet<ProductEntity> ProductEntities { get; set; }
    public virtual DbSet<ProductPhotoEntity> ProductPhotoEntities { get; set; }
    public virtual DbSet<ProductSizeTypeEntity> ProductSizeEntities { get; set; }
    public virtual DbSet<SizeEntity> SizeEntities { get; set; }
    public virtual DbSet<StyleEntity> StyleEntities { get; set; }

    public ZebraMainContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<ProductSizeTypeEntity>().HasKey(x => new {x.ProductId, x.SizeId});
    }
  }
}