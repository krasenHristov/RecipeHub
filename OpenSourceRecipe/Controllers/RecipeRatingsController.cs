using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSourceRecipes.Models;

namespace OpenSourceRecipe.Controllers;

[ApiController]
public class RecipeRatingsController(RecipeRatingsRepository recipeRatingsRepository) : ControllerBase
{
    [HttpPost("api/recipes/rating")]
    [Authorize]
    public async Task<ActionResult> PostRecipeRating(RecipeRatingDto ratingDetails)
    {
        try
        {
            int? userIdFromToken = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value!);

            if (userIdFromToken != ratingDetails.UserId)
            {
                return Unauthorized();
            }

            var newRating = await recipeRatingsRepository.CreateRecipeRating(ratingDetails);
            return CreatedAtAction(nameof(PostRecipeRating), newRating);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("api/recipes/rating")]
    [Authorize]
    public async Task<ActionResult> PatchRecipeRating(RecipeRatingDto ratingDetails)
    {
        try
        {
            int? userIdFromToken = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value!);

            if (userIdFromToken != ratingDetails.UserId)
            {
                return Unauthorized();
            }

            return Ok(await recipeRatingsRepository.UpdateRecipeRating(ratingDetails));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
