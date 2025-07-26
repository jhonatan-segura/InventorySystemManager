namespace ITSenseAPI.DTOs
{
   public class InventoryOutputDto
   {
      public int ProductId { get; set; }
      public int StockQuantity { get; set; }
      public string Reason { get; set; } = "Salida general";
   }

}
