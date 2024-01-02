using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSourceRecipes.Models;

public class Favourites
{
    // Foreign Key to the Recipe table
    public int RecipeId { get; set; }
    // Foreign Key to the User table
    public int UserId { get; set; }
}
