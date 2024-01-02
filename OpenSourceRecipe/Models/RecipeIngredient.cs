using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSourceRecipes.Models;

public class RecipeIngredient
{
    // Foreign Key to the Recipe table
    public int? RecipeId { get; set; }

    // Foreign Key to the Ingredient table
    public int? IngredientId { get; set; }

    // The quantity of the ingredient
    public string? Quantity { get; set; }
}
