using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSourceRecipes.Models;
using OpenSourceRecipes.Services;

namespace OpenSourceRecipe.Controllers;

[ApiController]
public class UserController(UserRepository userRepository) : ControllerBase
{

    [HttpPost("api/register")]
    public async Task<ActionResult<string>> RegisterUser(User user)
    {
        try
        {
            var checkUser = await userRepository.GetUserByUsername(user.Username!);

            if (checkUser != null)
            {
                return BadRequest("Username already exists");
            }

            return await userRepository.RegisterUser(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPost("api/login")]
    public async Task<ActionResult<string>> LoginUser(LoginUserDto user)
    {
        try
        {
            return await userRepository.SignUserIn(user.Username!, user.Password!);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/user/{username}")]
    public async Task<ActionResult<GetUserDto>> GetUserByUsername(string username)
    {
        try
        {
            var user = await userRepository.GetUserByUsername(username);

            if (user == null)
            {
                return NotFound("User does not exist");
            }

            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("api/user/id/{userId}")]
    public async Task<ActionResult<GetUserDto>> GetUserById(int userId)
    {
        try 
        {
            var user = await userRepository.GetUserById(userId);

            
            if (user == null)
            {
                return NotFound("User does not exist");
            }

            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("api/test-auth")]
    [Authorize]
    public IActionResult Test ()
    {
        return Ok();
    }
}