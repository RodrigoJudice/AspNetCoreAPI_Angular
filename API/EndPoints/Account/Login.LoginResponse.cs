namespace API.EndPoints.Account;

public class LoginResponse
{
  public string UserName { get; set; } = string.Empty;
  public string Token { get; set; } = string.Empty;
}