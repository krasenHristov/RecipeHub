using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using OpenSourceRecipes.Models;

[Collection("Sequential")]
public class CommentsTest
{
    private readonly HttpClient _client = SharedTestResources.Factory.CreateClient();


    // COMMENT TESTS
    [Fact]
    public async Task CreateCommentEndpoint_ShouldSucceed()
    {
        // Arrange
        var newComment = new
        {
            Comment = "Test Comment",
            RecipeId = 13,
            UserId = 1,
            Author = "seededuser"
        };

        var newUser = new
        {
            Username = "seededuser",
            Password = "password"
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json")
        };

        var loginResponse = await _client.SendAsync(loginRequest);
        var userDetails = await loginResponse.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user?.Token);

        var request = new HttpRequestMessage(HttpMethod.Post, "api/comments")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newComment), Encoding.UTF8, "application/json")
        };

        //Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task CreateCommentEndpointNoRecipe_ShouldFail()
    {
        // Arrange
        var newComment = new
        {
            Comment = "Test Comment",
            RecipeId = 9999,
            UserId = 1,
            Author = "seededuser"
        };

        var newUser = new
        {
            Username = "seededuser",
            Password = "password"
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json")
        };

        var loginResponse = await _client.SendAsync(loginRequest);
        var userDetails = await loginResponse.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user?.Token);

        var request = new HttpRequestMessage(HttpMethod.Post, "api/comments")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newComment), Encoding.UTF8, "application/json")
        };

        //Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetCommentsForRecipe_ShouldSucceed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/comments/1");
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetCommentsForRecipeNoRecipe_ShouldFail()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/comments/9999");
        var response = await _client.SendAsync(request);

        // assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCommentEndpoint_ShouldSucceed()
    {
        var userLogin = new
        {
            Username = "seededuser",
            Password = "password"
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(userLogin), Encoding.UTF8, "application/json")
        };

        var loginResponse = await _client.SendAsync(loginRequest);

        var userDetails = loginResponse.Content.ReadAsStringAsync().Result;

        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user?.Token);

        // get recipe by id
        var request = new HttpRequestMessage(HttpMethod.Delete, "api/comments/39");
        var response = await _client.SendAsync(request);

        // assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        var getCommentRequest = new HttpRequestMessage(HttpMethod.Get, "api/comments/39");
        var getCommentResponse = await _client.SendAsync(getCommentRequest);

        Assert.Equal(HttpStatusCode.NotFound, getCommentResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteCommentEndpointNoComment_ShouldFail()
    {
        var userLogin = new
        {
            Username = "seededuser",
            Password = "password"
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(userLogin), Encoding.UTF8, "application/json")
        };

        var loginResponse = await _client.SendAsync(loginRequest);

        var userDetails = loginResponse.Content.ReadAsStringAsync().Result;

        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user?.Token);

        // get recipe by id
        var request = new HttpRequestMessage(HttpMethod.Delete, "api/comments/9999");
        var response = await _client.SendAsync(request);

        // assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteAnotherUserComment_ShouldFail()
    {
        var userLogin = new
        {
            Username = "seededuser",
            Password = "password"
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(userLogin), Encoding.UTF8, "application/json")
        };

        var loginResponse = await _client.SendAsync(loginRequest);

        var userDetails = loginResponse.Content.ReadAsStringAsync().Result;

        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user?.Token);

        var request = new HttpRequestMessage(HttpMethod.Delete, "api/comments/40");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCommentBody_ShouldSucceed()
    {
        var userLogin = new
        {
            Username = "seededuser",
            Password = "password"
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(userLogin), Encoding.UTF8, "application/json")
        };

        var loginResponse = await _client.SendAsync(loginRequest);

        var userDetails = loginResponse.Content.ReadAsStringAsync().Result;

        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user?.Token);

        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/comments/20")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Comment = "This is a test comment update" }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        // deserialize response
        var content = await response.Content.ReadAsStringAsync();
        var comment = JsonConvert.DeserializeObject<GetCommentDto>(content);

        // Assert - Ensure the request was successful

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("This is a test comment update", comment!.Comment);
    }

    [Fact]
    public async Task UpdateCommentBodyWithWrongUser_ShouldFail()
    {
        // Arrange - login user
        var newUser = new
        {
            Username = "seededuser",
            Password = "password"
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json")
        };

        var loginResponse = await _client.SendAsync(loginRequest);
        loginResponse.EnsureSuccessStatusCode();

        var userDetails = loginResponse.Content.ReadAsStringAsync().Result;

        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user?.Token);

        // Act - Call the API
        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/comments/5")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Comment = "This is a test comment update" }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        // Assert - Ensure the request was successful
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCommentBodyWithNoComment_ShouldFail()
    {
        // Arrange - login user
        var newUser = new
        {
            Username = "jellyfish",
            Password = "password"
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json")
        };

        var loginResponse = await _client.SendAsync(loginRequest);
        loginResponse.EnsureSuccessStatusCode();

        var userDetails = loginResponse.Content.ReadAsStringAsync().Result;

        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user?.Token);

        // Act - Call the API
        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/comments/1")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Comment = "" }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        // Assert - Ensure the request was successful
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

}
