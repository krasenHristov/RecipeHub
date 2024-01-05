using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace OpenSourceRecipe.IntegrationTests;
public class RecipeEndpointTests(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task CreateRecipeEndpoint_ShouldSucceed()
    {
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
            UserId = 1,
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
}
