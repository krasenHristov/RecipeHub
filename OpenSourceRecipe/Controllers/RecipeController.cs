using Microsoft.AspNetCore.Mvc;
using OpenSourceRecipes.Models;
using OpenSourceRecipes.Services;

namespace OpenSourceRecipe.Controllers;

[ApiController]
public class RecipeController(RecipeRepository recipeRepository) : ControllerBase
{
    [HttpPost("api/recipes")]
    public async Task<ActionResult<GetRecipeDto>> CreateRecipe(CreateRecipeDto recipe)
    {
        try
        {
            return await recipeRepository.CreateRecipe(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/recipes")]
    public async Task<ActionResult<IEnumerable<GetRecipeDto>>> GetAllRecipes(int? userId = null, int? cuisineId = null)
    {
        try
        {
            return Ok(await recipeRepository.GetAllRecipes(userId, cuisineId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/recipes/{recipeId}")]
    public async Task<ActionResult<GetRecipeByIdDto>> GetRecipeById(int recipeId)
    {
        try
        {
            GetRecipeByIdDto? recipe = await recipeRepository.GetRecipeById(recipeId);

            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("api/recipes/{recipeId}")]
    public async Task<ActionResult> DeleteRecipe(int recipeId)
    {
        try
        {
            await recipeRepository.DeleteRecipe(recipeId);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/recipes/forks")]
    public async Task<ActionResult<IEnumerable<GetRecipeDto>>> GetForkedRecipes(
        int? userId = null, int? cuisineId = null, int? forkedFromId = null, int? originalRecipeId = null)
    {
        try
        {
            return Ok(await recipeRepository.GetForkedRecipes(userId, cuisineId, forkedFromId, originalRecipeId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
