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
            Status = true,
            Bio = "This is a test user for integration testing purposes only.............................................................."
        };

        var registerRequest = new HttpRequestMessage(HttpMethod.Post, "api/register")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json")
        };

        var registerResponse = await _client.SendAsync(registerRequest);
        registerResponse.EnsureSuccessStatusCode();

        var registerContent = await registerResponse.Content.ReadAsStringAsync();

        GetUserDto? user = JsonConvert.DeserializeObject<GetUserDto>(registerContent);

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
        var content = await response.Content.ReadAsStringAsync();

        var recipes = JsonConvert.DeserializeObject<List<GetRecipeDto>>(content);

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

        var content = JsonConvert.DeserializeObject<List<GetRecipeDto>>(contentString);

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

        var content = JsonConvert.DeserializeObject<List<GetRecipeDto>>(contentString);

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

        var content = JsonConvert.DeserializeObject<List<GetRecipeDto>>(contentString);

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

        var content = JsonConvert.DeserializeObject<List<GetRecipeDto>>(contentString);

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

        var content = JsonConvert.DeserializeObject<List<GetRecipeDto>>(contentString);

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
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/forks?cuisineId=3");
        var response = await _client.SendAsync(request);
        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipeDto>>(contentString);

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

        var content = JsonConvert.DeserializeObject<List<GetRecipeDto>>(contentString);

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

        var content = JsonConvert.DeserializeObject<List<GetRecipeDto>>(contentString);

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

        var content = JsonConvert.DeserializeObject<List<GetRecipeDto>>(contentString);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count == 0);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetAllForkedRecipesByForkedFromId_ShouldSucceed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/forks?forkedFromId=3");
        var response = await _client.SendAsync(request);
        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipeDto>>(contentString);

        foreach (var recipe in content!)
        {
            Assert.NotNull(recipe.ForkedFromId);
            Assert.NotNull(recipe.OriginalRecipeId);
            Assert.Equal(3, recipe.ForkedFromId);
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

        var content = JsonConvert.DeserializeObject<List<GetRecipeDto>>(contentString);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content!.Count == 0);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task GetAllForkedRecipesByOriginalRecipeId_ShouldSucceed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/forks?originalRecipeId=3");
        var response = await _client.SendAsync(request);
        var contentString = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<List<GetRecipeDto>>(contentString);

        foreach (var recipe in content!)
        {
            Assert.NotNull(recipe.ForkedFromId);
            Assert.NotNull(recipe.OriginalRecipeId);
            Assert.Equal(3, recipe.OriginalRecipeId);
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
        // get recipe by id
        var request = new HttpRequestMessage(HttpMethod.Delete, "api/recipes/1");
        var response = await _client.SendAsync(request);

        // assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        var getRecipeRequest = new HttpRequestMessage(HttpMethod.Get, "api/recipes/1");
        var getRecipeResponse = await _client.SendAsync(getRecipeRequest);

        Assert.Equal(HttpStatusCode.NotFound, getRecipeResponse.StatusCode);
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

}
