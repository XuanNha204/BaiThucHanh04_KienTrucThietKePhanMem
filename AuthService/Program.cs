using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Api;
using MediatR;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(_ => true).AllowCredentials()));
var app = builder.Build();
app.UseCors();
app.MapGet("/health", () => Results.Ok(new { service = "AuthService", status = "Healthy" }));
app.MapPost("/api/User", (LoginRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Password)) return Results.BadRequest(new { error = "Login and password are required" });
    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("THIS_IS_A_RANDOM_SECRET_2e7a1e80-16ee-4e52-b5c6-5e8892453459"));
    var descriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, request.Login), new Claim("userType", "SALESMAN") }),
        Expires = DateTime.UtcNow.AddHours(8), SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
    };
    var handler = new JwtSecurityTokenHandler();
    var token = handler.WriteToken(handler.CreateToken(descriptor));
    return Results.Ok(new LoginResponse
    {
        Token = token, UserName = request.Login, Roles = new[] { "SALESMAN" },
        UserType = "SALESMAN", ExpiresIn = DateTime.UtcNow.AddHours(8)
    });
});
app.MapGet("/api/internal/ping", async (ISender sender) => Results.Ok(await sender.Send(new PingQuery())));
app.Run();
public sealed record PingQuery : IRequest<string>;
public sealed class PingHandler : IRequestHandler<PingQuery, string>
{
    public Task<string> Handle(PingQuery request, CancellationToken cancellationToken) => Task.FromResult("pong");
}
public partial class Program { }
