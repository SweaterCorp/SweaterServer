using Microsoft.EntityFrameworkCore;
using ProductDatabase.Entities;

namespace ProductDatabase
{
  public class ProductContext : DbContext
  {
    public virtual DbSet<BrandEntity> BrandEntities { get; set; }
    public virtual DbSet<CategoryEntity> CategoryEntities { get; set; }
    public virtual DbSet<ProductEntity> ProductEntities { get; set; }
    public virtual DbSet<ProductPhotoEntity> ProductPhotoEntities { get; set; }
    public virtual DbSet<ProductSizeTypeEntity> ProductSizeTypeEntities { get; set; }
    public virtual DbSet<SizeTypeEntity> SizeTypeEntities { get; set; }
    public virtual DbSet<CountryEntity> CountryEntities { get; set; }
    public virtual DbSet<ColorGoodnessEntity> ColorGoodnessEntities { get; set; }
    public virtual DbSet<ProductColorGoodnessEntity> ProductColorGoodnessEntities { get; set; }
    public virtual DbSet<UserEntity> UserEntities { get; set; }

    public ProductContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //modelBuilder.Entity<UserEntity>().Property(a => a.HumanColorType).HasColumnType("int");
      //modelBuilder.Entity<UserEntity>().Property(a => a.HumanColorType).Ignore(x => x.HumanColorType.Name);

      //modelBuilder.Entity<UserEntity>().Property(a => a.SexType)..HasColumnType("int");
      //modelBuilder.Entity<UserEntity>().Ignore(x => x.SexType.Name);

      //modelBuilder.Entity<UserEntity>().Property(a => a.ShapeType).HasColumnType("int");
      //modelBuilder.Entity<UserEntity>().Ignore(x => x.ShapeType.Name);

      //modelBuilder.Entity<UserPhotoEntity>().HasKey(x => new { x. });
      modelBuilder.Entity<ProductSizeTypeEntity>().HasKey(x => new {x.ProductId, x.SizeTypeId});
      //modelBuilder.Entity<ProductColorTypeEntity>().HasKey(x => new { x.ProductId, x.ColorTypeId });
    }
  }
}