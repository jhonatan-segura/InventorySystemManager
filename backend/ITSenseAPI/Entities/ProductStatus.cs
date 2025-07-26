using System.ComponentModel.DataAnnotations;

namespace ITSenseAPI.Entities
{
   public class ProductStatus
   {
      public int Id { get; set; }

      [Required]
      [MaxLength(20)]
      public required string Name { get; set; }
      public ICollection<Product> Products { get; set; } = [];
      public int IsActive { get; set; } = 1;
   }
}