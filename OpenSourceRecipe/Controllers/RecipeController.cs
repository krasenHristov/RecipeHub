using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSourceRecipes.Models;
using OpenSourceRecipes.Services;

namespace OpenSourceRecipe.Controllers;

[ApiController]
public class RecipeController(RecipeRepository recipeRepository) : ControllerBase
{

    [HttpPost("api/recipes")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetRecipesDto>> CreateRecipe(CreateRecipeDto recipe)
    {
        try
        {
            // get user id
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")!.Value);

            // compare to passed id in recipe
            if (userId != recipe.UserId)
            {
                return BadRequest("User id does not match");
            }

            GetRecipesDto newRecipe = await recipeRepository.CreateRecipe(recipe);
            return Created($"api/recipes", newRecipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/recipes")]
    public async Task<ActionResult<IEnumerable<GetRecipesDto>>> GetAllRecipes(int? userId = null, int? cuisineId = null)
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
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteRecipe(int recipeId)
    {
        try
        {
            GetRecipeByIdDto? recipe = await recipeRepository.GetRecipeById(recipeId);

            if (recipe == null)
            {
                return NotFound();
            }

            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")!.Value);

            if (userId != recipe!.UserId)
            {
                return Unauthorized("Recipe belongs to another user");
            }

            await recipeRepository.DeleteRecipe(recipeId);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/recipes/forks")]
    public async Task<ActionResult<IEnumerable<GetRecipesDto>>> GetForkedRecipes(
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

    [HttpPatch("api/recipes")]
    //[Authorize]
    public async Task<ActionResult<GetRecipesDto>> PatchExistingRecipe(PatchRecipeDto recipeToPatchInfo)
    {
        try
        {
            int? userIdFromToken = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value!);

            if (userIdFromToken != recipeToPatchInfo.UserId)
            {
                return Unauthorized();
            }

            return Ok(await recipeRepository.PatchRecipe(recipeToPatchInfo));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
