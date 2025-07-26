using System.Security.Cryptography;

namespace ITSenseAPI.Utilities
{
   public static class PasswordHasher
   {
      public static byte[] HashPassword(string password, out byte[] salt)
      {
         salt = RandomNumberGenerator.GetBytes(16); // Salt de 128 bits
         using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
         var hash = pbkdf2.GetBytes(32); // Hash de 256 bits

         // Devolvemos hash + salt juntos en un solo array
         return [.. salt, .. hash];
      }

      public static bool VerifyPassword(string password, byte[] storedHashWithSalt)
      {
         var salt = storedHashWithSalt.Take(16).ToArray();
         var storedHash = storedHashWithSalt.Skip(16).ToArray();

         using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
         var computedHash = pbkdf2.GetBytes(32);

         return CryptographicOperations.FixedTimeEquals(storedHash, computedHash);
      }
   }
}