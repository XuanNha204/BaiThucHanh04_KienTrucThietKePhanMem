using Microsoft.AspNetCore.Mvc.Testing;
using PaymentService.Api;

namespace PaymentService.Test;
public sealed class ServiceTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;
    public ServiceTests(WebApplicationFactory<Program> factory) => client = factory.CreateClient();
    [Fact] public async Task Health_endpoint_is_available() => Assert.True((await client.GetAsync("/health")).IsSuccessStatusCode);
    [Fact] public void Payment_contract_keeps_amount() => Assert.Equal(125m, new RegisterPaymentCommand("POL-1", 125m, DateTime.UtcNow).Amount);
}
