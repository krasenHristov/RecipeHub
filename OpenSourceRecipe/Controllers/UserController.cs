using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSourceRecipes.Models;
using OpenSourceRecipes.Services;

namespace OpenSourceRecipe.Controllers;

[ApiController]
public class UserController(UserRepository userRepository) : ControllerBase
{

    [HttpPost("api/register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetLoggedInUserDto>> RegisterUser(User user)
    {
        try
        {
            var checkUser = await userRepository.GetUserByUsername(user.Username!);

            if (checkUser != null)
            {
                return BadRequest("Username already exists");
            }

            GetLoggedInUserDto userDetails = await userRepository.RegisterUser(user);

            return Created($"api/user/{user.Username}", userDetails);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPost("api/login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetLoggedInUserDto>> LoginUser(LoginUserDto user)
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public IActionResult Test ()
    {
        return Ok();
    }

    [HttpPatch("api/user/{userId}/bio")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<ActionResult<GetUserDto>> UpdateUser(int userId, UpdateUserBioDto bio)
    {
        try
        {
            if (string.IsNullOrEmpty(bio.Bio))
            {
                return BadRequest("Bio cannot be empty");
            }

            int? userIdFromToken = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value!);

            if (userIdFromToken != userId)
            {
                return Unauthorized();
            }

            var updatedUser = await userRepository.UpdateUserBio(userId, bio.Bio!);

            if (updatedUser == null)
            {
                return NotFound("User does not exist");
            }

            return Ok(updatedUser);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPatch("api/user/{userId}/img")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<ActionResult<GetUserDto>> UpdateUser(int userId, UpdateUserImgDto img)
    {
        try
        {
            if (string.IsNullOrEmpty(img.ProfileImg))
            {
                return BadRequest("Img cannot be empty");
            }

            int? userIdFromToken = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value!);

            if (userIdFromToken != userId)
            {
                return Unauthorized();
            }

            var updatedUser = await userRepository.UpdateUserImg(userId, img.ProfileImg!);

            if (updatedUser == null)
            {
                return NotFound("User does not exist");
            }

            return Ok(updatedUser);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPatch("api/user/{userId}/name")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<ActionResult<GetUserDto>> UpdateUser(int userId, UpdateUserNameDto name)
    {
        try
        {
            if (string.IsNullOrEmpty(name.Name))
            {
                return BadRequest("Name cannot be empty");
            }

            int? userIdFromToken = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value!);

            if (userIdFromToken != userId)
            {
                return Unauthorized();
            }

            var updatedUser = await userRepository.UpdateUserName(userId, name.Name!);

            if (updatedUser == null)
            {
                return NotFound("User does not exist");
            }

            return Ok(updatedUser);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
