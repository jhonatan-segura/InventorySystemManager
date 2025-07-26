namespace ITSenseAPI.Entities
{
   public class Product
   {
      public int Id { get; set; }
      public required string Name { get; set; }
      public required int StockQuantity { get; set; }

      public int TypeOfManufacturingId { get; set; }
      public TypeOfManufacturing TypeOfManufacturing { get; set; } = null!;

      public int ProductStatusId { get; set; }
      public ProductStatus? ProductStatus { get; set; }
      public ICollection<InventoryOutput> Outputs { get; set; } = [];
      public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;
      public int IsActive { get; set; } = 1;
   }
}