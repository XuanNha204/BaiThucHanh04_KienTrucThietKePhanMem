using Microsoft.AspNetCore.Mvc.Testing;
using PolicyService.Api.Commands;

namespace PolicyService.Test;
public sealed class ServiceTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;
    public ServiceTests(WebApplicationFactory<Program> factory) => client = factory.CreateClient();
    [Fact] public async Task Health_endpoint_is_available() => Assert.True((await client.GetAsync("/health")).IsSuccessStatusCode);
    [Fact] public void Offer_contract_has_cover_collection() => Assert.Empty(new CreateOfferCommand().SelectedCovers);
}
