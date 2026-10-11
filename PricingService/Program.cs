using MediatR;
using PricingService.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
var app = builder.Build();
app.MapGet("/health", () => Results.Ok(new { service = "PricingService", status = "Healthy" }));
app.MapPost("/api/pricing/calculate", (PriceCalculationRequest request) =>
{
    var days = Math.Max(1, (request.PolicyTo.Date - request.PolicyFrom.Date).Days);
    var baseRate = request.ProductCode.ToUpperInvariant() switch { "CAR" => 2.5m, "HOME" => 1.8m, "TRAVEL" => 0.8m, _ => 1.2m };
    return Results.Ok(new PriceCalculationResult(decimal.Round(days * baseRate, 2)));
});
app.MapGet("/api/internal/ping", async (ISender sender) => Results.Ok(await sender.Send(new PingQuery())));
app.Run();
public sealed record PingQuery : IRequest<string>;
public sealed class PingHandler : IRequestHandler<PingQuery, string>
{
    public Task<string> Handle(PingQuery request, CancellationToken cancellationToken) => Task.FromResult("pong");
}
public partial class Program { }
