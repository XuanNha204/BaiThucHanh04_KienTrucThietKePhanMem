using DashboardService.Api.Queries;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DashboardService.Test;
public sealed class ServiceTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;
    public ServiceTests(WebApplicationFactory<Program> factory) => client = factory.CreateClient();
    [Fact] public async Task Health_endpoint_is_available() => Assert.True((await client.GetAsync("/health")).IsSuccessStatusCode);
    [Fact] public void Dashboard_contract_initializes_totals() => Assert.NotNull(new GetTotalSalesResult().Total);
}
