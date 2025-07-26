using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ITSenseAPI.Entities
{
   [Index(nameof(Email), IsUnique = true)]
   public class User
   {
      public int Id { get; set; }
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
      public required byte[] Password { get; set; }
      public DateTime RegistrationDate { get; set; } = DateTime.UtcNow; 
   }
}