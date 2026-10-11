namespace AuthService.Api;

public sealed record LoginRequest(string Login, string Password);
public sealed class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string[] Roles { get; set; } = Array.Empty<string>();
    public string Avatar { get; set; } = "/static/avatars/agent1.png";
    public string UserType { get; set; } = "SALESMAN";
    public DateTime ExpiresIn { get; set; }
}
