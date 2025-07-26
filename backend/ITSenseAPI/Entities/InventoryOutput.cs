namespace ITSenseAPI.Entities
{
   public class InventoryOutput
   {
      public int Id { get; set; }
      public int ProductId { get; set; }
      public int StockQuantity { get; set; }
      public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;
      public string Reason { get; set; } = "Salida general";
      public Product Product { get; set; } = null!;
      public int IsActive { get; set; } = 1;
   }
}