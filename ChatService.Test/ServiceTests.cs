using ChatService.Api;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ChatService.Test;
public sealed class ServiceTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;
    public ServiceTests(WebApplicationFactory<Program> factory) => client = factory.CreateClient();
    [Fact] public async Task Health_endpoint_is_available() => Assert.True((await client.GetAsync("/health")).IsSuccessStatusCode);
    [Fact] public void Chat_contract_keeps_sender() => Assert.Equal("agent", new ChatMessageDto("agent", "hello", DateTime.UtcNow).Sender);
}
