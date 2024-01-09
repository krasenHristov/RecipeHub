using Microsoft.AspNetCore.Mvc;

namespace OpenSourceRecipe.Controllers;

[ApiController]
public class InsertImages(FirebaseStorageService.Storage.FirebaseStorageService firebaseStorageService) : ControllerBase
{

    [HttpPost("api/image")]
    public async Task<ActionResult<string>> UploadImage([FromForm] IFormFile file)
    {
        try
        {
            if (file.Length > 5242880)
            {
                return BadRequest("File size exceeds limit.");
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            using var stream = file.OpenReadStream();
            var imageUrl = await firebaseStorageService.UploadFileAsync(stream, fileName);

            return Ok(imageUrl);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
}
