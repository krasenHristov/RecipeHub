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


    [Fact]
    public async Task PatchRecipe_Title_ShouldSucceed()
    {
        //Arrange
        // Define patch object with UserId & modified parameter
        var modifiedRecipe = new
        {
            UserId = 1,
            RecipeId = 1,
            RecipeTitle = "New pizza Title"
        };

        //Act
        // 1. Login User
        var userToLogin = new
        {
            Username = "seededuser",
            Password = "password"
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "/api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(userToLogin), Encoding.UTF8, "application/json")
        };

        var loginResponse = await _client.SendAsync(loginRequest);
        loginResponse.EnsureSuccessStatusCode();

        var userDetails = await loginResponse.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        // 2. Request to patch title
        var request = new HttpRequestMessage(HttpMethod.Patch, "/api/recipes")
        {
            Content = new StringContent(JsonConvert.SerializeObject(modifiedRecipe), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", "Bearer " + user?.Token);

        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task PatchRecipe_Difficulty_ShouldSucceed()
    {
        //Arrange
        // Define patch object with UserId & modified parameter
        var modifiedRecipe = new
        {
            UserId = 1,
            RecipeId = 1,
            Difficulty = 5
        };

        //Act
        // 1. Login User
        var userToLogin = new
        {
            Username = "seededuser",
            Password = "password"
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "/api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(userToLogin), Encoding.UTF8, "application/json")
        };

        var loginResponse = await _client.SendAsync(loginRequest);
        loginResponse.EnsureSuccessStatusCode();

        var userDetails = await loginResponse.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        // 2. Request to patch title
        var request = new HttpRequestMessage(HttpMethod.Patch, "/api/recipes")
        {
            Content = new StringContent(JsonConvert.SerializeObject(modifiedRecipe), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", "Bearer " + user?.Token);

        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task PatchRecipe_TimeToPrepare_ShouldSucceed()
    {
        //Arrange
        // Define patch object with UserId & modified parameter
        var modifiedRecipe = new
        {
            UserId = 1,
            RecipeId = 1,
            TimeToPrepare = 3
        };

        //Act
        // 1. Login User
        var userToLogin = new
        {
            Username = "seededuser",
            Password = "password"
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "/api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(userToLogin), Encoding.UTF8, "application/json")
        };

        var loginResponse = await _client.SendAsync(loginRequest);
        loginResponse.EnsureSuccessStatusCode();

        var userDetails = await loginResponse.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        // 2. Request to patch title
        var request = new HttpRequestMessage(HttpMethod.Patch, "/api/recipes")
        {
            Content = new StringContent(JsonConvert.SerializeObject(modifiedRecipe), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", "Bearer " + user?.Token);

        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task PatchRecipe_MultipleParameters_ShouldSucceed()
    {
        //Arrange
        // Define patch object with UserId & modified parameter
        var modifiedRecipe = new
        {
            UserId = 1,
            RecipeId = 2,
            RecipeTitle = "New pizza",
            CuisineId = 2
        };

        //Act
        // 1. Login User
        var userToLogin = new
        {
            Username = "seededuser",
            Password = "password"
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "/api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(userToLogin), Encoding.UTF8, "application/json")
        };

        var loginResponse = await _client.SendAsync(loginRequest);
        loginResponse.EnsureSuccessStatusCode();

        var userDetails = await loginResponse.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        // 2. Request to patch title
        var request = new HttpRequestMessage(HttpMethod.Patch, "/api/recipes")
        {
            Content = new StringContent(JsonConvert.SerializeObject(modifiedRecipe), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", "Bearer " + user?.Token);

        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task PatchRecipe_BadParameter_ShouldFail()
    {
        //Arrange
        // Define patch object with UserId & modified parameter
        var modifiedRecipe = new
        {
            UserId = 1,
            RecipeId = 1,
            RecipeTitle = 5
        };

        //Act
        // 1. Login User
        var userToLogin = new
        {
            Username = "seededuser",
            Password = "password"
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "/api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(userToLogin), Encoding.UTF8, "application/json")
        };

        var loginResponse = await _client.SendAsync(loginRequest);
        loginResponse.EnsureSuccessStatusCode();

        var userDetails = await loginResponse.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        // 2. Request to patch title
        var request = new HttpRequestMessage(HttpMethod.Patch, "/api/recipes")
        {
            Content = new StringContent(JsonConvert.SerializeObject(modifiedRecipe), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", "Bearer " + user?.Token);

        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task AddIngredientsToRecipe_ShouldSucceed()
    {
        var body = new
        {
            IngredientIds = new int[] { 222, 333, 444 },
            Quantity = new string[] { "1 cup", "2 cups", "3 cups" }
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "api/ingredients/recipes/1/ingredients")
        {
            Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        var recipe = JsonConvert.DeserializeObject<GetRecipeByIdDto>(content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(recipe!.RecipeIngredients!.Count > 0);
    }

    /*
    [Fact]
    public async Task UpdateIngredientsForRecipe_ShouldSucceed()
    {

        var initialRecipe = new HttpRequestMessage(HttpMethod.Get, "api/recipes/11");

        var initialRecipeResponse = await _client.SendAsync(initialRecipe);
        var initialRecipeContent = await initialRecipeResponse.Content.ReadAsStringAsync();

        var initialRecipeObject = JsonConvert.DeserializeObject<GetRecipeByIdDto>(initialRecipeContent);

        Assert.True(initialRecipeObject!.RecipeIngredients!.Count > 3);

        var body = new
        {
            IngredientIds = new int[] { 122, 233, 344 },
            Quantity = new string[] { "1 cup", "2 cups", "3 cups" }
        };

        var request = new HttpRequestMessage(HttpMethod.Patch , "api/ingredients/recipes/11/ingredients")
        {
            Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        var recipe = new HttpRequestMessage(HttpMethod.Get, "api/recipes/11");

        var recipeResponse = await _client.SendAsync(recipe);
        var recipeContent = await recipeResponse.Content.ReadAsStringAsync();

        var recipeObject = JsonConvert.DeserializeObject<GetRecipeByIdDto>(recipeContent);

        foreach (var ingredient in recipeObject!.RecipeIngredients!)
        {
            Assert.Contains(ingredient.IngredientId, body.IngredientIds);
        }

        Assert.Equal(3, recipeObject!.RecipeIngredients!.Count);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

*/
}
