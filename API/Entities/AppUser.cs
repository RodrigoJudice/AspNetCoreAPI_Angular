using System.ComponentModel.DataAnnotations;
using LanguageExt;

namespace API.Entities;

public class AppUser
{
  [Key]
  public int Id { get; set; }
  public string UserName { get; set; } = string.Empty;
  public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
  public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

}
