using DashboardService.Api.Queries;
using DashboardService.Api.Queries.Dtos;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
var app = builder.Build();
var perProduct = new Dictionary<string, SalesDto>
{
    ["CAR"] = new() { PremiumAmount = 12500, PoliciesCount = 14 },
    ["HOME"] = new() { PremiumAmount = 8100, PoliciesCount = 10 },
    ["TRAVEL"] = new() { PremiumAmount = 3200, PoliciesCount = 22 },
    ["FARM"] = new() { PremiumAmount = 6700, PoliciesCount = 5 }
};
app.MapGet("/health", () => Results.Ok(new { service = "DashboardService", status = "Healthy" }));
app.MapPost("/api/Dashboard/total-sales", (GetTotalSalesQuery query) => Results.Ok(new GetTotalSalesResult
{
    Total = new SalesDto { PremiumAmount = perProduct.Values.Sum(x => x.PremiumAmount), PoliciesCount = perProduct.Values.Sum(x => x.PoliciesCount) },
    PerProductTotal = perProduct
}));
app.MapPost("/api/Dashboard/agents-sales", (GetAgentsSalesQuery query) => Results.Ok(new GetAgentsSalesResult
{
    PerAgentTotal = new Dictionary<string, SalesDto> { ["agent1"] = new() { PremiumAmount = 17500, PoliciesCount = 25 }, ["agent2"] = new() { PremiumAmount = 13000, PoliciesCount = 18 } }
}));
app.MapPost("/api/Dashboard/sales-trends", (GetSalesTrendsQuery query) => Results.Ok(new GetSalesTrendsResult
{
    PeriodsSales = new List<PeriodSalesDto> { new() { Period = "2026-08", Sales = new() { PremiumAmount = 8200, PoliciesCount = 10 } }, new() { Period = "2026-09", Sales = new() { PremiumAmount = 9700, PoliciesCount = 13 } }, new() { Period = "2026-10", Sales = new() { PremiumAmount = 12600, PoliciesCount = 20 } } }
}));
app.MapGet("/api/internal/ping", async (ISender sender) => Results.Ok(await sender.Send(new PingQuery())));
app.Run();
public sealed record PingQuery : IRequest<string>;
public sealed class PingHandler : IRequestHandler<PingQuery, string>
{
    public Task<string> Handle(PingQuery request, CancellationToken cancellationToken) => Task.FromResult("pong");
}
public partial class Program { }
