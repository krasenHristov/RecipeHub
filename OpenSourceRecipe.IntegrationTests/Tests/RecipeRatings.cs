using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using OpenSourceRecipes.Models;

[Collection("Sequential")]
public class RecipeRatings
{
    private readonly HttpClient _client = SharedTestResources.Factory.CreateClient();

    [Fact]
    public async Task CreateRecipeRatingById_ShouldSucceed()
    {
        // Arrange
        var newRating = new
        {
            RecipeId = 11,
            UserId = 1,
            Rating = 3,
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

        var request = new HttpRequestMessage(HttpMethod.Post, "api/recipes/rating")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newRating), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", "Bearer " + user?.Token);

        //Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task CreateRecipeRatingById_InvalidRecipeId_ShouldFail()
    {
        //400 bad request response for bad recipeId
        // Arrange
        var newRating = new
        {
            RecipeId = "Not Valid",
            UserId = 1,
            Rating = 3,
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

        var request = new HttpRequestMessage(HttpMethod.Post, "api/recipes/rating")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newRating), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", "Bearer " + user?.Token);

        //Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task CreateRecipeRatingById_InvalidUserId_ShouldFail()
    {
        //400 bad request response for bad userId
        // Arrange
        var newRating = new
        {
            RecipeId = 1,
            UserId = "Not valid",
            Rating = 3,
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

        var request = new HttpRequestMessage(HttpMethod.Post, "api/recipes/rating")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newRating), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", "Bearer " + user?.Token);

        //Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(content);

    }

    [Fact]
    public async Task CreateRecipeRatingById_InvalidRating_ShouldFail()
    {
        //400 bad request response for bad rating
        // Arrange
        var newRating = new
        {
            RecipeId = 1,
            UserId = 1,
            Rating = 9999,
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

        var request = new HttpRequestMessage(HttpMethod.Post, "api/recipes/rating")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newRating), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", "Bearer " + user?.Token);

        //Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(content);

    }

    [Fact]
    public async Task UpdateRecipeRatingById_ShouldSucceed()
    {
        //200 Response for UpdateRecipeRatingByID with valid params
        // Arrange
        var newRating = new
        {
            RecipeId = 1,
            UserId = 1,
            Rating = 3,
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

        var request = new HttpRequestMessage(HttpMethod.Post, "api/recipes/rating")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newRating), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", "Bearer " + user?.Token);

        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        var patchRating = new
        {
            RecipeId = 1,
            UserId = 1,
            Rating = 5,
        };

        var patchRequest = new HttpRequestMessage(HttpMethod.Patch, "api/recipes/rating")
        {
            Content = new StringContent(JsonConvert.SerializeObject(patchRating), Encoding.UTF8, "application/json")
        };
        patchRequest.Headers.Add("Authorization", "Bearer " + user?.Token);

        var patchResponse = await _client.SendAsync(patchRequest);
        var patchContent = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.OK, patchResponse.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task UpdateRecipeRatingById_InvalidRecipeId_ShouldFail()
    {
        //400 bad request response for for bad recipeId
                // Arrange
        var newRating = new
        {
            RecipeId = "Not Valid",
            UserId = 1,
            Rating = 3,
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

        var request = new HttpRequestMessage(HttpMethod.Patch, "api/recipes/rating")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newRating), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", "Bearer " + user?.Token);

        //Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(content);

    }

    [Fact]
    public async Task UpdateRecipeRatingById_InvalidUserId_ShouldFail()
    {
        //400 bad request response for bad userId
        // Arrange
        var newRating = new
        {
            RecipeId = 1,
            UserId = "Not valid",
            Rating = 3,
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

        var request = new HttpRequestMessage(HttpMethod.Patch, "api/recipes/rating")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newRating), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", "Bearer " + user?.Token);

        //Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task UpdateRecipeRatingById_InvalidRating_ShouldFail()
    {
        //400 bad request response for bad rating
        // Arrange
        var newRating = new
        {
            RecipeId = 1,
            UserId = 1,
            Rating = 9999,
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

        var request = new HttpRequestMessage(HttpMethod.Patch, "api/recipes/rating")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newRating), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", "Bearer " + user?.Token);

        //Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetRecipesByIngredientIds_ShouldSucceed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes?recipeIds=120%220%3");
        var response = await _client.SendAsync(request);
        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipesDto>>(contentString);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count > 0);
        Assert.NotNull(content);
    }
}
