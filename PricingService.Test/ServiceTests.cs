using Microsoft.AspNetCore.Mvc.Testing;
using PricingService.Api;

namespace PricingService.Test;
public sealed class ServiceTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;
    public ServiceTests(WebApplicationFactory<Program> factory) => client = factory.CreateClient();
    [Fact] public async Task Health_endpoint_is_available() => Assert.True((await client.GetAsync("/health")).IsSuccessStatusCode);
    [Fact] public void Price_contract_uses_euro_by_default() => Assert.Equal("EUR", new PriceCalculationResult(10).Currency);
}
