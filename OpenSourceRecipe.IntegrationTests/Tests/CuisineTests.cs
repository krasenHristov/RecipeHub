using System.Net;
using Newtonsoft.Json;
using OpenSourceRecipes.Models;

[Collection("Sequential")]
public class CuisineTests
{
    private readonly HttpClient _client = SharedTestResources.Factory.CreateClient();

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
