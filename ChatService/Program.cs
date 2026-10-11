using ChatService.Api;
using MediatR;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddSignalR();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(_ => true).AllowCredentials()));
var app = builder.Build();
app.UseCors();
app.MapGet("/health", () => Results.Ok(new { service = "ChatService", status = "Healthy" }));
app.MapHub<ChatHub>("/agentsChat");
app.MapGet("/api/internal/ping", async (ISender sender) => Results.Ok(await sender.Send(new PingQuery())));
app.Run();

public sealed class ChatHub : Hub
{
    public Task SendMessage(string message)
    {
        var sender = Context.User?.Identity?.Name ?? "agent";
        return Clients.All.SendAsync("ReceiveMessage", sender, "/static/avatars/agent1.png", message);
    }
}
public sealed record PingQuery : IRequest<string>;
public sealed class PingHandler : IRequestHandler<PingQuery, string>
{
    public Task<string> Handle(PingQuery request, CancellationToken cancellationToken) => Task.FromResult("pong");
}
public partial class Program { }
