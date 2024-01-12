/*
using System.Net;
using System.Text;
using Newtonsoft.Json;

[Collection("Sequential")]
public class RecipeEndpoints
{

    private readonly HttpClient _client = SharedTestResources.Factory.CreateClient();

    // INGREDIENT TESTS
    [Fact]
    public async Task CreateIngredientEndpoint_ShouldSucceed()
    {
        //Arrange
        var newIngredient = new
        {
            IngredientName = "testing new ingredient",
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
*/
