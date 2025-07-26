using System.ComponentModel.DataAnnotations;

namespace ITSenseAPI.DTOs
{
   public class UserValidatedDto
   {
      public int Id { get; set; }
      public required string Name { get; set; }
      public required string Email { get; set; }
      public required string Token { get; set; }
   }
}
