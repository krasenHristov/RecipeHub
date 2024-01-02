using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSourceRecipes.Models;

public class RecipeComment
{
    // Primary Key
    public int? CommentId { get; set; }

    // Foreign Key to the Recipe table
    public int? RecipeId { get; set; }

    // Foreign Key to the User table
    public int? UserId { get; set; }

    // The comment itself
    public string? Comment { get; set; }

    // The date the comment was posted defaults to the current date
    public string PostedOn { get; set; } = DateTime.Now.ToString("MM/dd/yyyy");
}
