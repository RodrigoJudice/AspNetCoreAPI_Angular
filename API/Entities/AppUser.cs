using System.ComponentModel.DataAnnotations;
using LanguageExt;

namespace API.Entities;

public class AppUser
{
  [Key]
  public int Id { get; set; }

  [Required]
  public string UserName { get; set; } = string.Empty;
  [Required]
  public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

  [Required]
  public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

}
