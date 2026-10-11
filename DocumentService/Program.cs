using System.Text;
using DocumentService.Api;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
var app = builder.Build();
app.MapGet("/health", () => Results.Ok(new { service = "DocumentService", status = "Healthy" }));
app.MapGet("/api/Documents/{policyNumber}", (string policyNumber) => Results.Ok(new
{
    documents = new[] { new PolicyDocumentDto(policyNumber, $"Policy-{policyNumber}.pdf", Convert.ToBase64String(Encoding.UTF8.GetBytes($"Policy certificate: {policyNumber}"))) }
}));
app.MapGet("/api/internal/ping", async (ISender sender) => Results.Ok(await sender.Send(new PingQuery())));
app.Run();
public sealed record PingQuery : IRequest<string>;
public sealed class PingHandler : IRequestHandler<PingQuery, string>
{
    public Task<string> Handle(PingQuery request, CancellationToken cancellationToken) => Task.FromResult("pong");
}
public partial class Program { }
