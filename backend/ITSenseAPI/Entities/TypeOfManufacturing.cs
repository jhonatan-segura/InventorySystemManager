namespace ITSenseAPI.Entities
{
   public class TypeOfManufacturing
   {
      public int Id { get; set; }
      public string Name { get; set; } = null!;
      public ICollection<Product> Products { get; set; } = new List<Product>();
      public int IsActive { get; set; } = 1;
   }
}