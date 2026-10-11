using Microsoft.AspNetCore.Mvc.Testing;
using PolicySearchService.Api.Queries;

namespace PolicySearchService.Test;
public sealed class ServiceTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;
    public ServiceTests(WebApplicationFactory<Program> factory) => client = factory.CreateClient();
    [Fact] public async Task Health_endpoint_is_available() => Assert.True((await client.GetAsync("/health")).IsSuccessStatusCode);
    [Fact] public void Search_contract_has_result_collection() => Assert.Empty(new FindPolicyResult().Policies);
}
