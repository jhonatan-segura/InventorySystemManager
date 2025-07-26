namespace ITSenseAPI.DTOs
{
   public class GetProductDto
   {
      public int Id { get; set; }
      public string Name { get; set; } = null!;
      public required int StockQuantity { get; set; }
      public int TypeOfManufacturingId { get; set; }
      public string TypeOfManufacturingName { get; set; } = null!;
      public int ProductStatusId { get; set; }
      public string ProductStatusName { get; set; } = null!;
   }

}
