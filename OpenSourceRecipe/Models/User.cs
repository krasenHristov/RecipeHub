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
    public string? ProfileImg { get; set; }

    // Password of the user
    [MaxLength(255)]
    [MinLength(6)]
    public string? Password { get; set; }

    // Status of the user (active or inactive) (true or false)
    public bool Status { get; set; } = true;

    // Bio of the user
    [MinLength(100)]
    public string? Bio { get; set; }
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
