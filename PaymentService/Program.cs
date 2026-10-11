using System.Collections.Concurrent;
using MediatR;
using PaymentService.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
var app = builder.Build();
var accounts = new ConcurrentDictionary<string, PolicyAccountDto>();
app.MapGet("/health", () => Results.Ok(new { service = "PaymentService", status = "Healthy" }));
app.MapPost("/api/payments/accounts/{policyNumber}", (string policyNumber) =>
{
    var account = accounts.GetOrAdd(policyNumber, p => new PolicyAccountDto(p, $"ACC-{Random.Shared.Next(100000, 999999)}", 0));
    return Results.Ok(account);
});
app.MapPost("/api/payments", (RegisterPaymentCommand command) =>
{
    var account = accounts.AddOrUpdate(command.PolicyNumber,
        p => new PolicyAccountDto(p, $"ACC-{Random.Shared.Next(100000, 999999)}", command.Amount),
        (_, current) => current with { Balance = current.Balance + command.Amount });
    return Results.Ok(account);
});
app.MapGet("/api/internal/ping", async (ISender sender) => Results.Ok(await sender.Send(new PingQuery())));
app.Run();
public sealed record PingQuery : IRequest<string>;
public sealed class PingHandler : IRequestHandler<PingQuery, string>
{
    public Task<string> Handle(PingQuery request, CancellationToken cancellationToken) => Task.FromResult("pong");
}
public partial class Program { }
