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

public class CreateCommentDto
{
    public int? RecipeId { get; set; }

    public int? UserId { get; set; }

    public string? Author { get; set; }

    public string? Comment { get; set; }
}

public class GetCommentDto
{
    public int? CommentId { get; set; }

    public int? RecipeId { get; set; }

    public int? UserId { get; set; }

    public string? Comment { get; set; }

    public string? Author { get; set; }

    public string? PostedOn { get; set; }
}
