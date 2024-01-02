using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace OpenSourceRecipe.IntegrationTests;

public class UserEndpoints(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

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

    [Fact]
    public async Task RegisterNewUserWithSameUsername_ShouldFail()
    {
        // arrange
        var newUser = new
        {
            Username = "testuserr",
            Name = "Test Userr",
            ProfileImg = "https://www.google.com",
            Password = "password",
            Bio = "This is a test user for integration testing purposes only :)........................................"
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "api/register")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json")
        };

        var request2 = new HttpRequestMessage(HttpMethod.Post, "api/register")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json")
        };

        // act
        var response = await _client.SendAsync(request);
        var response2 = await _client.SendAsync(request2);
        var content = await response2.Content.ReadAsStreamAsync();

        // assert
        Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task LoginUser_ShouldSucceed()
    {
        // arrange
        var newUser = new
        {
            Username = "testuserrr",
            Name = "Test Userrr",
            ProfileImg = "https://www.google.com",
            Password = "password",
            Bio = "This is a test user for integration testing purposes only :)........................................"
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "api/register")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json")
        };

        var request2 = new HttpRequestMessage(HttpMethod.Post, "api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { newUser.Username, newUser.Password }), Encoding.UTF8, "application/json")
        };

        // act
        var response = await _client.SendAsync(request);
        var response2 = await _client.SendAsync(request2);
        var content = await response2.Content.ReadAsStreamAsync();

        // assert
        Assert.Equal(HttpStatusCode.OK, response2.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task LoginUserWithWrongPassword_ShouldFail()
    {
        // arrange
        var newUser = new
        {
            Username = "testuserrrr",
            Name = "Test Userrrr",
            ProfileImg = "https://www.google.com",
            Password = "password",
            Bio = "This is a test user for integration testing purposes only :)........................................"
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "api/register")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json")
        };

        var request2 = new HttpRequestMessage(HttpMethod.Post, "api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { newUser.Username, Password = "wrongpassword" }), Encoding.UTF8, "application/json")
        };

        // act
        var response = await _client.SendAsync(request);
        var response2 = await _client.SendAsync(request2);
        var content = await response2.Content.ReadAsStreamAsync();

        // assert
        Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task LoginUserWithWrongUsername_ShouldFail()
    {
        // arrange
        var newUser = new
        {
            Username = "testuserrrrr",
            Name = "Test Userrrrr",
            ProfileImg = "https://www.google.com",
            Password = "password",
            Bio = "This is a test user for integration testing purposes only :)........................................"
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "api/register")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json")
        };

        var request2 = new HttpRequestMessage(HttpMethod.Post, "api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Username = "wrongusername", newUser.Password }), Encoding.UTF8, "application/json")
        };

        // act
        var response = await _client.SendAsync(request);
        var response2 = await _client.SendAsync(request2);
        var content = await response2.Content.ReadAsStreamAsync();

        // assert
        Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task AuthTestEndpointWithToken_ShouldSucceed()
    {
        // Arrange - Register a new user
        var newUser = new
        {
            Username = "testuserrrrrr",
            Name = "Test Userrrrrr",
            ProfileImg = "https://www.google.com",
            Password = "password",
            Bio = "This is a test user for integration testing purposes only.............................................................."
        };

        var registerRequest = new HttpRequestMessage(HttpMethod.Post, "api/register")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json")
        };

        var registerResponse = await _client.SendAsync(registerRequest);
        registerResponse.EnsureSuccessStatusCode();

        string token = registerResponse.Content.ReadAsStringAsync().Result;

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act - Call the API
        var request = new HttpRequestMessage(HttpMethod.Get, "api/test");
        var response = await _client.SendAsync(request);

        // Assert - Ensure the request was successful
        response.EnsureSuccessStatusCode();
    }
}
