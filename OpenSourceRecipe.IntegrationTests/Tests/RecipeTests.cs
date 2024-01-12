using System.Net;
using Newtonsoft.Json;
using OpenSourceRecipes.Models;

namespace OpenSourceRecipe.IntegrationTests;

[Collection("Sequential")]
public class RecipeEndpoints
{

    private readonly HttpClient _client = SharedTestResources.Factory.CreateClient();


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
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/13");
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
    public async Task GetRelevantRecipes_ShouldSucceed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/recipes/relevant/13");
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
