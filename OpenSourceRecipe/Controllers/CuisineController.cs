using Microsoft.AspNetCore.Mvc;
using OpenSourceRecipes.Models;
using OpenSourceRecipes.Services;

namespace OpenSourceRecipe.Controllers;

[ApiController]
public class CuisineController(CuisineRepository cuisineRepository) : ControllerBase
{
    [HttpGet("api/cuisines")]
    public async Task<ActionResult<IEnumerable<GetCuisineDto>>> GetAllCuisines()
    {
        try
        {
            return Ok(await cuisineRepository.GetAllCuisines());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
