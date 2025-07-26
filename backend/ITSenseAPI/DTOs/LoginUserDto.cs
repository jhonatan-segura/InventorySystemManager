using System.ComponentModel.DataAnnotations;

namespace ITSenseAPI.DTOs
{
   public class LoginUserDto
   {
      [Required]
      [EmailAddress]
      [MaxLength(30)]
      public required string Email { get; set; }

      [Required]
      [MinLength(8)]
      public required string Password { get; set; }
   }
}
