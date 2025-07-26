namespace ITSenseAPI.DTOs
{
   public class MoveProductDto
   {
      public int ProductId { get; set; }
      public int StockQuantity { get; set; }
      public string Reason { get; set; } = string.Empty;
   }
}