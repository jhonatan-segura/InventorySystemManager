using ITSenseAPI.Entities;

namespace ITSenseAPI.DTOs
{
   public class GetInventoryOutputDto
   {
      public int Id { get; set; }
      public required GetProductIdNameDto Product { get; set; } = null!;
      public int StockQuantity { get; set; }
      public string Reason { get; set; } = "Salida general";
   }

   public class GetProductIdNameDto
   {
      public int Id { get; set; }
      public required string Name { get; set; }
   }

}
