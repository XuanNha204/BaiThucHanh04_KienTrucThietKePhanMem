using MediatR;
using PolicySearchService.Api.Queries;
using PolicySearchService.Api.Queries.Dtos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
var app = builder.Build();
var policies = new List<PolicyDto>
{
    new() { PolicyNumber = "POL-DEMO-0001", ProductCode = "CAR", PolicyStartDate = DateTime.Today, PolicyEndDate = DateTime.Today.AddYears(1), PolicyHolder = "Demo Customer", PremiumAmount = 912.50m }
};
app.MapGet("/health", () => Results.Ok(new { service = "PolicySearchService", status = "Healthy" }));
app.MapGet("/api/PolicySearch", (string? q) =>
{
    var result = string.IsNullOrWhiteSpace(q) ? policies : policies.Where(p =>
        p.PolicyNumber.Contains(q, StringComparison.OrdinalIgnoreCase) || p.PolicyHolder.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList();
    return Results.Ok(new FindPolicyResult { Policies = result.ToList() });
});
app.MapGet("/api/internal/ping", async (ISender sender) => Results.Ok(await sender.Send(new PingQuery())));
app.Run();
public sealed record PingQuery : IRequest<string>;
public sealed class PingHandler : IRequestHandler<PingQuery, string>
{
    public Task<string> Handle(PingQuery request, CancellationToken cancellationToken) => Task.FromResult("pong");
}
public partial class Program { }
