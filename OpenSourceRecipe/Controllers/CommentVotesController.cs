using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSourceRecipes.Models;
using OpenSourceRecipes.Services;

namespace OpenSourceRecipe.Controllers;

[ApiController]
public class CommentVotesController(CommentVotesRepository commentVotesRepository, IConfiguration configuration) : ControllerBase
{
    private IConfiguration _configuration = configuration;

    [HttpPost("api/commentvotes/{commentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<ActionResult<string>> CreateCommentVote(int commentId, CommentVoteDto voteDetails)
    {
        try
        {
            var commentRepo = new CommentRepository(_configuration);

            GetCommentDto? comment = await commentRepo.GetCommentById(commentId);

            if (comment == null)
            {
                return NotFound("Comment does not exist.");
            }

            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value!);

            string result = await commentVotesRepository.HandleCommentVote(commentId, userId, voteDetails);

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
