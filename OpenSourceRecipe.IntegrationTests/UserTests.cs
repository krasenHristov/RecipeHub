using System.Net;
using Xunit;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;

namespace OpenSourceRecipe.IntegrationTests;

public class UserEndpoints : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UserEndpoints(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
    }

    [Fact]
    public async Task AuthTestEndpointWithoutToken_ShouldFail()
    {
        // arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "api/test");

        // act
        var response = await _client.SendAsync(request);

        // assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task RegisterNewUser_ShouldSucceed()
    {
        // arrange
        var newUser = new
        {
            Username = "testuser",
            Name = "Test User",
            ProfileImg = "https://www.google.com",
            Password = "password",
            Bio = "This is a test user for integration testing purposes only :)........................................"
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "api/register")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json")
        };

        // act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        response.EnsureSuccessStatusCode();
        Assert.NotNull(content);
    }
}
