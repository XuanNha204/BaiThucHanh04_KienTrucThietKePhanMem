using Microsoft.AspNetCore.Mvc.Testing;
using ProductService.Api.Queries.Dtos;

namespace ProductService.Test;
public sealed class ServiceTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;
    public ServiceTests(WebApplicationFactory<Program> factory) => client = factory.CreateClient();
    [Fact] public async Task Health_endpoint_is_available() => Assert.True((await client.GetAsync("/health")).IsSuccessStatusCode);
    [Fact] public void Product_contract_initializes_collections() { var product = new ProductDto(); Assert.Empty(product.Covers); Assert.Empty(product.Questions); }
}
