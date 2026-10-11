using AuthService.Api;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AuthService.Test;
public sealed class ServiceTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;
    public ServiceTests(WebApplicationFactory<Program> factory) => client = factory.CreateClient();
    [Fact] public async Task Health_endpoint_is_available() => Assert.True((await client.GetAsync("/health")).IsSuccessStatusCode);
    [Fact] public void Login_contract_keeps_credentials() => Assert.Equal("agent", new LoginRequest("agent", "pass").Login);
}
