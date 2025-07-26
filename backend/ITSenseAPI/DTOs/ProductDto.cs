
namespace ITSenseAPI.DTOs
{
   public class ProductDto
   {
      public string Name { get; set; } = null!;
      public int StockQuantity { get; set; }
      public int TypeOfManufacturingId { get; set; }
      public int ProductStatusId { get; set; }
   }
}