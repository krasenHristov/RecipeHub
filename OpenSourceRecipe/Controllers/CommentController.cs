using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSourceRecipes.Models;
using OpenSourceRecipes.Services;

namespace OpenSourceRecipe.Controllers;

[ApiController]
public class CommentController(CommentRepository commentRepository) : ControllerBase
{

    [HttpPost("api/comments")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<ActionResult<GetCommentDto?>> CreateComment(CreateCommentDto comment)
    {
        try
        {
            string? username = User.Claims.FirstOrDefault(c => c.Type == "Username")?.Value;

            if (username != comment.Author)
            {
                return Unauthorized();
            }

            GetCommentDto? newComment = await commentRepository.CreateComment(comment);

            return CreatedAtAction(nameof(CreateComment), newComment);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/comments/{recipeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<GetCommentDto>>> GetCommentsByRecipeId(int recipeId)
    {
        try
        {
            return Ok(await commentRepository.GetCommentsByRecipeId(recipeId));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}
