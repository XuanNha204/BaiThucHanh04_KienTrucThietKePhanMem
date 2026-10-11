using DocumentService.Api;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DocumentService.Test;
public sealed class ServiceTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;
    public ServiceTests(WebApplicationFactory<Program> factory) => client = factory.CreateClient();
    [Fact] public async Task Health_endpoint_is_available() => Assert.True((await client.GetAsync("/health")).IsSuccessStatusCode);
    [Fact] public void Document_contract_keeps_policy_number() => Assert.Equal("POL-1", new PolicyDocumentDto("POL-1", "policy.pdf", "AA==").PolicyNumber);
}
