using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZebraData.Entities
{
  [Table("Product")]
  public class ProductEntity
  {
    [Key]
    public int ProductId { get; set; }

    public int BrandId { get; set; }
    public string VendorCode { get; set; }
    public int CategoryId { get; set; }
    public int StyleId { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public int MadeInCountryId { get; set; }
    public string Link { get; set; }
    public string PhotoPreviewUrl { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }
  }
}