using System.Collections.Concurrent;
using MediatR;
using PolicyService.Api.Commands;
using PolicyService.Api.Queries;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
var app = builder.Build();

var offers = new ConcurrentDictionary<string, (CreateOfferCommand Command, decimal Price)>();
var policies = new ConcurrentDictionary<string, PolicyDetailsDto>();

app.MapGet("/health", () => Results.Ok(new { service = "PolicyService", status = "Healthy" }));
app.MapPost("/api/Offers", (CreateOfferCommand command) =>
{
    var number = $"OFF-{Guid.NewGuid():N}"[..16].ToUpperInvariant();
    var price = decimal.Round(Math.Max(1, (command.PolicyTo.Date - command.PolicyFrom.Date).Days) * 2.5m, 2);
    offers[number] = (command, price);
    return Results.Ok(new CreateOfferResult { OfferNumber = number, TotalPrice = price });
});
app.MapPost("/api/Policies", (CreatePolicyCommand command) =>
{
    if (!offers.TryGetValue(command.OfferNumber, out var offer)) return Results.BadRequest(new { error = "Offer not found" });
    var number = $"POL-{DateTime.UtcNow:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}";
    policies[number] = new PolicyDetailsDto
    {
        Number = number, DateFrom = offer.Command.PolicyFrom, DateTo = offer.Command.PolicyTo,
        PolicyHolder = $"{command.PolicyHolder.FirstName} {command.PolicyHolder.LastName}".Trim(),
        TotalPremium = offer.Price, ProductCode = offer.Command.ProductCode,
        AccountNumber = $"ACC-{Random.Shared.Next(100000, 999999)}", Covers = offer.Command.SelectedCovers
    };
    return Results.Ok(new CreatePolicyResult { PolicyNumber = number });
});
app.MapGet("/api/Policies/{number}", (string number) => policies.TryGetValue(number, out var policy)
    ? Results.Ok(new GetPolicyDetailsQueryResult { Policy = policy })
    : Results.NotFound());
app.MapGet("/api/internal/ping", async (ISender sender) => Results.Ok(await sender.Send(new PingQuery())));
app.Run();
public sealed record PingQuery : IRequest<string>;
public sealed class PingHandler : IRequestHandler<PingQuery, string>
{
    public Task<string> Handle(PingQuery request, CancellationToken cancellationToken) => Task.FromResult("pong");
}
public partial class Program { }
