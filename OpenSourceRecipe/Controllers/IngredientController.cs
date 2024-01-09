using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSourceRecipes.Models;
using OpenSourceRecipes.Services;

namespace OpenSourceRecipe.Controllers;

[ApiController]
public class IngredientController(IngredientRepository ingredientRepository) : ControllerBase
{
    [HttpPost("api/ingredients")]
    public async Task<ActionResult<GetIngredientDto>> CreateIngredient(CreateIngredientDto ingredient)
    {
        try
        {
            return await ingredientRepository.CreateIngredient(ingredient);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/ingredients")]
    public async Task<ActionResult<IEnumerable<GetIngredientDto>>> GetAllIngredients()
    {
        try
        {
            return Ok(await ingredientRepository.GetAllIngredients());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("api/ingredients/recipes/{recipeId}/ingredients")]
    public async Task<ActionResult<GetRecipeByIdDto>> AddIngredientToRecipe(int recipeId, AddIngredientToRecipeDto? ingredient)
    {
        try
        {
            GetRecipeByIdDto? updatedRecipe = await ingredientRepository.AddIngredientsToRecipe(recipeId, ingredient?.IngredientIds!, ingredient?.Quantity!);

            return Ok(updatedRecipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("api/ingredients/recipes/{recipeId}/ingredients")]
    public async Task<ActionResult<GetRecipeByIdDto>> DeleteIngredientFromRecipe(int recipeId, AddIngredientToRecipeDto? ingredient)
    {
        try
        {
            await ingredientRepository.UpdateIngredientsForRecipe(recipeId, ingredient?.IngredientIds!, ingredient?.Quantity!);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
