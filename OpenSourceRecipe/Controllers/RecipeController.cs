using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSourceRecipes.Models;
using OpenSourceRecipes.Services;

namespace OpenSourceRecipe.Controllers;

[ApiController]
public class RecipeController(RecipeRepository recipeRepository) : ControllerBase
{
    [HttpPost("api/recipes")]
    public async Task<ActionResult<Recipe>> CreateRecipe(CreateRecipeDto recipe)
    {
        Console.WriteLine("Hi");
        try
        {
            return await recipeRepository.CreateRecipe(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}