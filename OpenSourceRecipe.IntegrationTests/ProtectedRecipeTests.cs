using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using OpenSourceRecipes.Models;

namespace OpenSourceRecipe.IntegrationTests;

[Collection("Sequential")]
public class ProtectedRecipeEndpoints
{

    private readonly HttpClient _client = SharedTestResources.Factory.CreateClient();


    // RECIPE TESTS

    [Fact]
    public async Task CreateRecipeEndpoint_ShouldSucceed()
    {

        // login user
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

        var userDetails = loginResponse.Content.ReadAsStringAsync().Result;

        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user?.Token);

        //Arrange
        var newRecipe = new
        {
            RecipeTitle = "Test Recipe",
            TagLine = "Yummy xUnit Test",
            Difficulty = 1,
            TimeToPrepare = 1,
            RecipeMethod = "1. Pass this test",
            RecipeImg = "https://i.redd.it/iewa8k3fl3d61.jpg",
            Cuisine = "I don't think we need this field, we have CuisineId",
            user!.UserId,
            CuisineId = 1,
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "api/recipes")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newRecipe), Encoding.UTF8, "application/json")
        };

        //Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task CreateRecipeEndpointNoRecipe_ShouldFail()
    {
        //Arrange
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

        //Act
        var request = new HttpRequestMessage(HttpMethod.Post, "api/recipes")
        {
            Content = new StringContent("", Encoding.UTF8, "application/json")
        };
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteRecipeEndpoint_ShouldSucceed()
    {
        var userLogin = new
        {
            Username = "jellyfish",
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
        var request = new HttpRequestMessage(HttpMethod.Delete, "api/recipes/23");
        var response = await _client.SendAsync(request);

        // assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        var getRecipeRequest = new HttpRequestMessage(HttpMethod.Get, "api/recipes/23");
        var getRecipeResponse = await _client.SendAsync(getRecipeRequest);

        Assert.Equal(HttpStatusCode.NotFound, getRecipeResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteRecipeEndpointNoRecipe_ShouldFail()
    {
        var userLogin = new
        {
            Username = "jellyfish",
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
        var request = new HttpRequestMessage(HttpMethod.Delete, "api/recipes/99");
        var response = await _client.SendAsync(request);

        // assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteAnotherUserRecipe_ShouldFail()
    {
        var userLogin = new
        {
            Username = "jellyfish",
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

        var request = new HttpRequestMessage(HttpMethod.Delete, "api/recipes/9");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

}
