using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OpenSourceRecipes.Models;

public class RecipeRating
{
    // Foreign Key to the Recipe table
    public int? RecipeId { get; set; }

    // Foreign Key to the User table
    public int? UserId { get; set; }

    // The rating the user gave the recipe
    [Range(1, 5)]
    public int? Rating { get; set; }
}

public class RecipeRatingDto
{
    public int? RecipeId { get; set; }
    public int? UserId { get; set; }
    public int? Rating { get; set; }
}