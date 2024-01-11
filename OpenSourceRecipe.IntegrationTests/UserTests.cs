using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Identity.Client;
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
    public async Task UpdateUserBio_ShouldSucceed()
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
        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/user/{user!.UserId}/bio")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Bio = "This is a test bio" }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        // Assert - Ensure the request was successful
        response.EnsureSuccessStatusCode();

        var getUserRequest = new HttpRequestMessage(HttpMethod.Get, "api/user/seededuser");
        var getUserResponse = await _client.SendAsync(getUserRequest);

        var content = await getUserResponse.Content.ReadAsStringAsync();

        var getUser = JsonConvert.DeserializeObject<GetUserDto>(content);

        Assert.Equal("This is a test bio", getUser?.Bio);
    }

    [Fact]
    public async Task UpdateUserBioWithWrongUser_ShouldFail()
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
        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/user/9999/bio")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Bio = "This is a test bio" }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        // Assert - Ensure the request was successful
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUserBioWithNoBio_ShouldFail()
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
        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/user/{user!.UserId}/bio")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Bio = "" }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        // Assert - Ensure the request was successful
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUserBioUnauthorized_ShouldFail()
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/user/1/bio")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Bio = "This is a test bio" }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUserImg_ShouldSucceed()
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
        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/user/{user!.UserId}/img")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { ProfileImg = "https://www.google.com" }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        // Assert - Ensure the request was successful
        response.EnsureSuccessStatusCode();

        var getUserRequest = new HttpRequestMessage(HttpMethod.Get, "api/user/seededuser");
        var getUserResponse = await _client.SendAsync(getUserRequest);

        var content = await getUserResponse.Content.ReadAsStringAsync();

        var getUser = JsonConvert.DeserializeObject<GetUserDto>(content);

        Assert.Equal("https://www.google.com", getUser?.ProfileImg);
    }

    [Fact]
    public async Task UpdateUserImgWithWrongUser_ShouldFail()
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
        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/user/9999/img")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { ProfileImg = "https://www.google.com" }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        // Assert - Ensure the request was successful
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUserImgWithNoImg_ShouldFail()
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
        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/user/{user!.UserId}/img")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { ProfileImg = "" }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        // Assert - Ensure the request was successful
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUserImgUnauthorized_ShouldFail()
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/user/1/img")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { ProfileImg = "https://www.google.com" }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUserName_ShouldSucceed()
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
        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/user/{user!.UserId}/name")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Name = "This is a test name" }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        // Assert - Ensure the request was successful
        response.EnsureSuccessStatusCode();

        var getUserRequest = new HttpRequestMessage(HttpMethod.Get, "api/user/seededuser");
        var getUserResponse = await _client.SendAsync(getUserRequest);

        var content = await getUserResponse.Content.ReadAsStringAsync();

        var getUser = JsonConvert.DeserializeObject<GetUserDto>(content);

        Assert.Equal("This is a test name", getUser?.Name);
    }

    [Fact]
    public async Task UpdateUserNameWithWrongUser_ShouldFail()
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
        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/user/9999/name")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Name = "This is a test name" }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        // Assert - Ensure the request was successful
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUserNameWithNoName_ShouldFail()
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
        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/user/{user!.UserId}/name")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Name = "" }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        // Assert - Ensure the request was successful
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUserNameUnauthorized_ShouldFail()
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/user/1/name")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Name = "This is a test name" }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
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
    public async Task GetAllRecipesEndpoint_ShouldSucceed()
    {
        // get all recipes
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes");
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        var recipes = JsonConvert.DeserializeObject<List<GetRecipesDto>>(content);

        foreach (var recipe in recipes!)
        {
            Assert.Null(recipe.ForkedFromId);
            Assert.Null(recipe.OriginalRecipeId);
        }

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAllRecipesByCuisine_ShouldSucceed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes?cuisineId=1");
        var response = await _client.SendAsync(request);
        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipesDto>>(contentString);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count > 0);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetAllRecipesByCuisine_ShouldReturnAnEmptyList()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes?cuisineId=9999");
        var response = await _client.SendAsync(request);

        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipesDto>>(contentString);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count == 0);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetAllRecipesByUser_ShouldSucceed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes?userId=1");
        var response = await _client.SendAsync(request);
        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipesDto>>(contentString);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count > 0);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetAllRecipesByUser_ShouldReturnAnEmptyList()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes?userId=9999");
        var response = await _client.SendAsync(request);

        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipesDto>>(contentString);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count == 0);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetAllForkedRecipes_ShouldSucceed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/forks");
        var response = await _client.SendAsync(request);
        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipesDto>>(contentString);

        foreach (var recipe in content!)
        {
            Assert.NotNull(recipe.ForkedFromId);
            Assert.NotNull(recipe.OriginalRecipeId);
        }

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count > 0);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetAllForkedRecipesByCuisine_ShouldSucceed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/forks?cuisineId=1");
        var response = await _client.SendAsync(request);
        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipesDto>>(contentString);

        foreach (var recipe in content!)
        {
            Assert.NotNull(recipe.ForkedFromId);
            Assert.NotNull(recipe.OriginalRecipeId);
        }

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count > 0);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetAllForkedRecipesByCuisine_ShouldReturnAnEmptyList()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/forks?cuisineId=9999");
        var response = await _client.SendAsync(request);

        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipesDto>>(contentString);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count == 0);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetAllForkedRecipesByUser_ShouldSucceed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/forks?userId=1");
        var response = await _client.SendAsync(request);
        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipesDto>>(contentString);

        foreach (var recipe in content!)
        {
            Assert.NotNull(recipe.ForkedFromId);
            Assert.NotNull(recipe.OriginalRecipeId);
            Assert.Equal(1, recipe.UserId);
        }

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count > 0);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetAllForkedRecipesByUser_ShouldReturnAnEmptyList()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/forks?userId=9999");
        var response = await _client.SendAsync(request);

        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipesDto>>(contentString);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count == 0);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetAllForkedRecipesByForkedFromId_ShouldSucceed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/forks?forkedFromId=7");
        var response = await _client.SendAsync(request);
        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipesDto>>(contentString);

        foreach (var recipe in content!)
        {
            Assert.NotNull(recipe.ForkedFromId);
            Assert.NotNull(recipe.OriginalRecipeId);
            Assert.Equal(7, recipe.ForkedFromId);
        }

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count > 0);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetAllForkedRecipesByForkedFromId_ShouldReturnAnEmptyList()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/forks?forkedFromId=9999");
        var response = await _client.SendAsync(request);

        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipesDto>>(contentString);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count == 0);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetAllForkedRecipesByOriginalRecipeId_ShouldSucceed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/forks?originalRecipeId=7");
        var response = await _client.SendAsync(request);
        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipesDto>>(contentString);

        foreach (var recipe in content!)
        {
            Assert.NotNull(recipe.ForkedFromId);
            Assert.NotNull(recipe.OriginalRecipeId);
            Assert.Equal(7, recipe.OriginalRecipeId);
        }

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count > 0);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetRecipeByIdEndpoint_ShouldSucceed()
    {
        // get recipe by id
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/1");
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetRecipeByIdEndpointNoRecipe_ShouldFail()
    {
        // get recipe by id
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/99");
        var response = await _client.SendAsync(request);

        // assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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
        var request = new HttpRequestMessage(HttpMethod.Delete, "api/recipes/1");
        var response = await _client.SendAsync(request);

        // assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        var getRecipeRequest = new HttpRequestMessage(HttpMethod.Get, "api/recipes/1");
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
    public async Task GetRelevantRecipes_ShouldSucceed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/relevant/1");
        var response = await _client.SendAsync(request);
        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRelevantRecipesDto>>(contentString);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count == 5);
    }

    [Fact]
    public async Task GetRelevantRecipes_ShouldReturnAnEmptyList()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/relevant/9999");
        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // INGREDIENT TESTS
    [Fact]
    public async Task CreateIngredientEndpoint_ShouldSucceed()
    {
       //Arrange
       var newIngredient = new
       {
            IngredientName = "Test Ingredient",
            Calories = "50 kcal",
            Carbohydrate = "5 g",
            Sugar = "3 g",
            Fiber = "0 g",
            Fat = "20 g",
            Protein = "10 g"
       };

       var request = new HttpRequestMessage(HttpMethod.Post, "api/ingredients")
       {
            Content = new StringContent(JsonConvert.SerializeObject(newIngredient), Encoding.UTF8, "application/json")
       };

       //Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        using (var reader = new StreamReader(content))
        {
            var contents = await reader.ReadToEndAsync();

            Console.WriteLine(contents);
        }


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

    [Fact]
    public async Task GetAllIngredientsEndpoint_ShouldSucceed()
    {
        // get all ingredients
        var request = new HttpRequestMessage(HttpMethod.Get, "api/ingredients");
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task AddIngredientsToRecipe_ShouldSucceed()
    {
        var body = new
        {
            IngredientIds = new int[] { 1, 2, 3 },
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
        Assert.True(recipe!.RecipeIngredients!.Count > 4);
    }

    [Fact]
    public async Task UpdateIngredientsForRecipe_ShouldSucceed()
    {

        var initialRecipe = new HttpRequestMessage(HttpMethod.Get, "api/recipes/1");

        var initialRecipeResponse = await _client.SendAsync(initialRecipe);
        var initialRecipeContent = await initialRecipeResponse.Content.ReadAsStringAsync();

        var initialRecipeObject = JsonConvert.DeserializeObject<GetRecipeByIdDto>(initialRecipeContent);

        Assert.True(initialRecipeObject!.RecipeIngredients!.Count > 3);

        var body = new
        {
            IngredientIds = new int[] { 1, 2, 3 },
            Quantity = new string[] { "1 cup", "2 cups", "3 cups" }
        };

        var request = new HttpRequestMessage(HttpMethod.Patch , "api/ingredients/recipes/1/ingredients")
        {
            Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        var recipe = new HttpRequestMessage(HttpMethod.Get, "api/recipes/1");

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

    [Fact]
    public async Task GetRecipesByIngredientIdsParamsWithComas_ShouldSucceed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes?recipeIds=1,2,3");
        var response = await _client.SendAsync(request);
        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipesDto>>(contentString);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count > 0);
        Assert.NotNull(content);
    }

    // CUISINE TESTS
    [Fact]
    public async Task GetAllCuisines_ShouldSucceed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/cuisines");
        var response = await _client.SendAsync(request);

        var contentString = await response.Content.ReadAsStringAsync();

        var cuisines = JsonConvert.DeserializeObject<List<GetCuisineDto>>(contentString);
        int length = cuisines!.Count;

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(length > 0);
        Assert.NotNull(cuisines);
    }


    // COMMENT TESTS
    [Fact]
    public async Task CreateCommentEndpoint_ShouldSucceed()
    {
        // Arrange
        var newComment = new
        {
            Comment = "Test Comment",
            RecipeId = 1,
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
        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/comments/20")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Comment = "" }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);

        // Assert - Ensure the request was successful
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // RECIPE RATING TESTS
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

    // COMMENT VOTE TESTS
    [Fact]
    public async Task HandleCommentVote_ShouldSucceed()
    {
        // Arrange
        var newCommentVote = new
        {
            Upvote = true,
        };

        var newUser = new
        {
            Username = "seededuser",
            Password = "password"
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "/api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json")
        };

        var loginResponse = await _client.SendAsync(loginRequest);
        loginResponse.EnsureSuccessStatusCode();

        var userDetails = await loginResponse.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        var request = new HttpRequestMessage(HttpMethod.Post, "/api/commentvotes/20")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newCommentVote), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", "Bearer " + user?.Token);

        //Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Upvote added.", content);

        var requestDownvote = new HttpRequestMessage(HttpMethod.Post, "/api/commentvotes/20")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Upvote = false }), Encoding.UTF8, "application/json")
        };
        requestDownvote.Headers.Add("Authorization", "Bearer " + user?.Token);

        var responseDownvote = await _client.SendAsync(requestDownvote);

        var contentDownvote = await responseDownvote.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, responseDownvote.StatusCode);
        Assert.Equal("Down-vote added.", contentDownvote);

        // add another downvote to remove it
        var requestDownvote2 = new HttpRequestMessage(HttpMethod.Post, "/api/commentvotes/20")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Upvote = false }), Encoding.UTF8, "application/json")
        };

        requestDownvote2.Headers.Add("Authorization", "Bearer " + user?.Token);

        var responseDownvote2 = await _client.SendAsync(requestDownvote2);

        var contentDownvote2 = await responseDownvote2.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, responseDownvote2.StatusCode);
        Assert.Equal("Down-vote removed.", contentDownvote2);

        // add another upvote
        var requestUpvote2 = new HttpRequestMessage(HttpMethod.Post, "/api/commentvotes/20")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Upvote = true }), Encoding.UTF8, "application/json")
        };

        requestUpvote2.Headers.Add("Authorization", "Bearer " + user?.Token);

        var responseUpvote2 = await _client.SendAsync(requestUpvote2);

        var contentUpvote2 = await responseUpvote2.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, responseUpvote2.StatusCode);
        Assert.Equal("Upvote added.", contentUpvote2);

        // add another upvote to remove it
        var requestUpvote3 = new HttpRequestMessage(HttpMethod.Post, "/api/commentvotes/20")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Upvote = true }), Encoding.UTF8, "application/json")
        };

        requestUpvote3.Headers.Add("Authorization", "Bearer " + user?.Token);

        var responseUpvote3 = await _client.SendAsync(requestUpvote3);

        var contentUpvote3 = await responseUpvote3.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, responseUpvote3.StatusCode);
        Assert.Equal("Upvote removed.", contentUpvote3);
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
            RecipeTitle = "New Recipe Title"
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
            RecipeTitle = "New Recipe Title",
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
    public async Task SearchRecipe_ShouldSucceed()
    {
        var recipes = new HttpRequestMessage(HttpMethod.Get, "api/recipes/search?search=pizza");
        var response = await _client.SendAsync(recipes);
        var content = await response.Content.ReadAsStringAsync();

        var recipesList = JsonConvert.DeserializeObject<IEnumerable<GetRecipesDto>>(content);

        foreach (var recipe in recipesList!)
        {
            Assert.Contains("pizza", recipe.RecipeTitle!.ToLower());
        }

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task SearchRecipeInMethod_ShouldSucceed()
    {
        var recipes = new HttpRequestMessage(HttpMethod.Get, "api/recipes/search?search=cook");
        var response = await _client.SendAsync(recipes);
        var content = await response.Content.ReadAsStringAsync();

        var recipesList = JsonConvert.DeserializeObject<IEnumerable<GetRecipesDto>>(content);

        foreach (var recipe in recipesList!)
        {
            Assert.Contains("cook", recipe.RecipeMethod!.ToLower());
        }

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task SearchRecipeByCuisine_ShouldSucceed()
    {
        var recipes = new HttpRequestMessage(HttpMethod.Get, "api/recipes/search?search=italian");
        var response = await _client.SendAsync(recipes);
        var content = await response.Content.ReadAsStringAsync();

        var recipesList = JsonConvert.DeserializeObject<IEnumerable<GetRecipesDto>>(content);

        foreach (var recipe in recipesList!)
        {
            Assert.Contains("italian", recipe.Cuisine!.ToLower());
        }

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task SearchRecipe_ShouldFail()
    {
        var recipes = new HttpRequestMessage(HttpMethod.Get, "api/recipes/search?search=notarealrecipe");
        var response = await _client.SendAsync(recipes);
        var content = await response.Content.ReadAsStringAsync();

        var recipesList = JsonConvert.DeserializeObject<IEnumerable<GetRecipesDto>>(content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Empty(recipesList!);
    }

}
