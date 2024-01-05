using System.ComponentModel.DataAnnotations;

namespace OpenSourceRecipes.Models;

public class User
{
    [MaxLength(255)]
    [MinLength(3)]
    public string? Username { get; set; }

    [MaxLength(255)]
    [MinLength(3)]
    public string? Name { get; set; }

    // Image of the user
    public string? ProfileImg { get; set; } = "https://www.outsystems.com/Forge_CW/_image.aspx/Q8LvY--6WakOw9afDCuuGdL9c3WA3ttAt5pfSB[…]-upload-example-2023-01-04%2000-00-00-2023-07-24%2020-02-59";

    // Password of the user
    [MaxLength(255)]
    [MinLength(6)]
    public string? Password { get; set; }

    // Status of the user (active or inactive) (true or false)
    public bool Status { get; set; } = true;

    // Bio of the user
    public string? Bio { get; set; } = "This user has not set a bio yet";
}

public class GetUserDto
{
    public int? UserId { get; set; }
    public string? Username { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public string? ProfileImg { get; set; }
    public bool Status { get; set; } = true;
    public string? Bio { get; set; }
}

public class LoginUserDto
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
