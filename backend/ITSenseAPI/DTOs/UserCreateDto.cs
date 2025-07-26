using System.ComponentModel.DataAnnotations;

namespace ITSenseAPI.DTOs
{
   public class UserCreateUpdateDto
   {
      [Required]
      [MaxLength(100)]
      public required string FirstName { get; set; }

      [Required]
      [MaxLength(100)]
      public required string LastName { get; set; }

      [Required]
      [EmailAddress]
      [MaxLength(30)]
      public required string Email { get; set; }

      [Required]
      [MinLength(8)]
      public required string Password { get; set; }
   }
}
