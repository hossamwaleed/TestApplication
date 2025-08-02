using System.ComponentModel.DataAnnotations;

namespace TestApplication.Authentication;

public class JwtOptions
{
   public static string JwtSectionName = "jwt";
    [Required]
    public string Key { get; set; }
    [Required]
    public string Audiance { get; set; }
    [Required]
    public string Issuer { get; set; }
    [Range(1,int.MaxValue,ErrorMessage = "invalid range number"),]
    public int ExpiryMinutes { get; set; }
}
