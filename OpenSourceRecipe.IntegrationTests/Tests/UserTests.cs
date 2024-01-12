using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using OpenSourceRecipes.Models;

namespace OpenSourceRecipe.IntegrationTests;

[Collection("Sequential")]
public class UserEndpoints
{

    private readonly HttpClient _client = SharedTestResources.Factory.CreateClient();

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
}
