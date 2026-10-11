using MediatR;
using ProductService.Api.Queries.Dtos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
var app = builder.Build();

var products = new List<ProductDto>
{
    new()
    {
        Code = "CAR", Name = "Car insurance", Description = "Vehicle protection",
        Image = "/static/car.jpg", Icon = "Car",
        Covers = new List<CoverDto> { new() { Code = "OC", Name = "Third-party liability", SumInsured = 100000 } },
        Questions = new List<QuestionDto>
        {
            new NumericQuestionDto { QuestionCode = "CAR_VALUE", Text = "Vehicle value" },
            new ChoiceQuestionDto
            {
                QuestionCode = "USAGE", Text = "Vehicle usage",
                Choices = new List<ChoiceDto> { new() { Code = "PRIVATE", Label = "Private" }, new() { Code = "BUSINESS", Label = "Business" } }
            }
        }
    },
    new() { Code = "HOME", Name = "Home insurance", Description = "Home and belongings", Image = "/static/house.jpg", Icon = "Home", Covers = new List<CoverDto> { new() { Code = "FIRE", Name = "Fire", SumInsured = 200000 } } },
    new() { Code = "TRAVEL", Name = "Travel insurance", Description = "Travel medical protection", Image = "/static/travel.jpg", Icon = "Travel", Covers = new List<CoverDto> { new() { Code = "MED", Name = "Medical expenses", SumInsured = 50000 } } },
    new() { Code = "FARM", Name = "Farm insurance", Description = "Agricultural property protection", Image = "/static/farm.jpg", Icon = "Farm", Covers = new List<CoverDto> { new() { Code = "PROPERTY", Name = "Farm property", SumInsured = 300000 } } }
};

app.MapGet("/health", () => Results.Ok(new { service = "ProductService", status = "Healthy" }));
app.MapGet("/api/Products", () => Results.Ok(products));
app.MapGet("/api/Products/{code}", (string code) =>
    products.FirstOrDefault(p => p.Code.Equals(code, StringComparison.OrdinalIgnoreCase)) is { } product
        ? Results.Ok(product)
        : Results.NotFound());
app.MapGet("/api/internal/ping", async (ISender sender) => Results.Ok(await sender.Send(new PingQuery())));
app.Run();

public sealed record PingQuery : IRequest<string>;
public sealed class PingHandler : IRequestHandler<PingQuery, string>
{
    public Task<string> Handle(PingQuery request, CancellationToken cancellationToken) => Task.FromResult("pong");
}
public partial class Program { }
