using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductDatabase.Entities
{
  [Table("Product")]
  public class ProductEntity
  {
    [Key]
    public int ProductId { get; set; }

    public int BrandId { get; set; }
    public int ShopColorId { get; set; }
    public int ShopTypeId { get; set; }
    public int ClicksCount { get; set; }
    public string VendorCode { get; set; }
    public bool IsAvailable { get; set; }
    public int CategoryId { get; set; }
    public int PrintTypeId { get; set; }
    public int ExtraPrintTypeId { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int MadeInCountryId { get; set; }
    public string Link { get; set; }
    public int PreviewPhotoId { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }
  }
}