using System.ComponentModel.DataAnnotations;

namespace API.EndPoints.Account;

public class CreateAccountRequest
{
  [Required]
  public string UserName { get; set; } = string.Empty;
  [Required]
  public string Password { get; set; } = string.Empty;

}