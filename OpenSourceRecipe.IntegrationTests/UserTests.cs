using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using OpenSourceRecipes.Models;

namespace OpenSourceRecipe.IntegrationTests;

public class UserEndpoints(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task AuthTestEndpointWithoutToken_ShouldFail()
    {
        // arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "api/test-auth");

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

        var userDetails = registerResponse.Content.ReadAsStringAsync().Result;

        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user?.Token);

        // Act - Call the API
        var request = new HttpRequestMessage(HttpMethod.Get, "api/test-auth");
        var response = await _client.SendAsync(request);

        // Assert - Ensure the request was successful
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetUserByUsername_ShouldReturnUserObject()
    {
        var newUser = new
        {
            Username = "testuserrrrrrr",
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

        var request = new HttpRequestMessage(HttpMethod.Get, $"api/user/testuserrrrrrr");

        var response = await _client.SendAsync(request);

        var content = await response.Content.ReadAsStringAsync();

        User? user = JsonConvert.DeserializeObject<User>(content.ToString());

        Assert.Equal("testuserrrrrrr", user?.Username);
        Assert.Equal("Test Userrrrrr", user?.Name);
        Assert.Equal("https://www.google.com", user?.ProfileImg);
        Assert.Equal("This is a test user for integration testing purposes only..............................................................", user?.Bio);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetUserById_ShouldSucceed()
    {
        //Arrange
          //Register user with username
        var newUser = new
        {
            Username = "testuser2",
            Name = "Test User2",
            Password = "password",
        };
          //Get user by username - then get ID
        var registerRequest = new HttpRequestMessage(HttpMethod.Post, "api/register")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json")
        };

        var registerResponse = await _client.SendAsync(registerRequest);
        registerResponse.EnsureSuccessStatusCode();

        var request = new HttpRequestMessage(HttpMethod.Get, $"api/user/testuser2");

        var response = await _client.SendAsync(request);

        var content = await response.Content.ReadAsStringAsync();

        GetUserDto? user = JsonConvert.DeserializeObject<GetUserDto>(content.ToString());

        //Act
          //Search user by ID - get ID
        var getUserByIdRequest = new HttpRequestMessage(HttpMethod.Get, $"api/user/id/{user!.UserId}");

        var userByIdResponse = await _client.SendAsync(getUserByIdRequest);

        var userByIdContent = await response.Content.ReadAsStringAsync();

        GetUserDto? userById = JsonConvert.DeserializeObject<GetUserDto>(userByIdContent.ToString());
        //Assert
          //Check returned user is registered user
        Assert.Equal(HttpStatusCode.OK, userByIdResponse.StatusCode);
        Assert.Equal("testuser2", userById!.Username);
        Assert.Equal("Test User2", userById.Name);
        Assert.Equal("https://www.outsystems.com/Forge_CW/_image.aspx/Q8LvY--6WakOw9afDCuuGdL9c3WA3ttAt5pfSB[…]-upload-example-2023-01-04%2000-00-00-2023-07-24%2020-02-59", userById.ProfileImg);
        Assert.Equal("This user has not set a bio yet", userById.Bio);
    }

    [Fact]
    public async Task GetUserByIdNoUser_ShouldFail()
    {
        //Act
            //Send request with wrong user ID
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/user/id/99999999999999");

        var response = await _client.SendAsync(request);

        //Assert
            //Assert bad request
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // RECIPE TESTS
    [Fact]
    public async Task CreateRecipeEndpoint_ShouldSucceed()
    {
        var newUser = new
        {
            Username = "testeruser4",
            Name = "Test User2",
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

        var requestUser = new HttpRequestMessage(HttpMethod.Get, $"api/user/testeruser4");

        var responseUser = await _client.SendAsync(requestUser);

        var userContent= await responseUser.Content.ReadAsStringAsync();

        GetUserDto? user = JsonConvert.DeserializeObject<GetUserDto>(userContent);

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
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task CreateRecipeEndpointNoRecipe_ShouldFail()
    {
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
    public async Task GetAllRecipesEndpoint_ShouldSucceed()
    {
        // get all recipes
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes");
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(content);
    }

    // INGREDIENT TESTS

    [Fact]
    public async Task CreateIngredientEndpoint_ShouldSucceed()
    {
       //Arrange
       var newIngredient = new
       {
            IngredientName = "Test Ingredient",
            Calories = 50,
            Carbohydrates = 5,
            Sugar = 10,
            Fiber = 10,
            Fat = 20,
            Protein = 10
       };

       var request = new HttpRequestMessage(HttpMethod.Post, "api/ingredients")
       {
            Content = new StringContent(JsonConvert.SerializeObject(newIngredient), Encoding.UTF8, "application/json")
       };

       //Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

       //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task CreateIngredientEndpointNoParams_ShouldFail()
    {
        //Act
        var request = new HttpRequestMessage(HttpMethod.Post, "api/ingredients")
        {
            Content = new StringContent("", Encoding.UTF8, "application/json")
        };
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
