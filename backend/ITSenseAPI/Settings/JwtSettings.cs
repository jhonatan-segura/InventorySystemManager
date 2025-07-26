namespace FinanzautoAPI.Settings
{
   public class JwtSettings
   {
      public string Key { get; set; } = default!;
      public string Issuer { get; set; } = default!;
      public string Audience { get; set; } = default!;
      public int ExpirationHours { get; set; } = 1;
   }
}
