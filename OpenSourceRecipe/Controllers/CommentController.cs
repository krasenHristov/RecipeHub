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

            Console.WriteLine(username);

            return await commentRepository.CreateComment(comment);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
