using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace OpenSourceRecipes.Models;

public class Ingredient
{
    // Primary Key
    public int IngredientId { get; set; }

    // Foreign Key to the Recipe table
    public string? IngredientName { get; set; }

    // Nutrition information for the ingredient
    public int Calories { get; set; }
    public int Carbohydrate { get; set; }
    public int Sugar { get; set; }
    public int Fiber { get; set; }
    public int Fat { get; set; }
    public int Protein { get; set; }
}

public class CreateIngredientDto
{
    public string? IngredientName { get; set; }
    public int Calories { get; set; }
    public int Carbohydrate { get; set; }
    public int Sugar { get; set; }
    public int Fiber { get; set; }
    public int Fat { get; set; }
    public int Protein { get; set; }
}

public class GetIngredientDto
{
    public int IngredientId { get; set; }
    public string? IngredientName { get; set; }
    public int Calories { get; set; }
    public int Carbohydrate { get; set; }
    public int Sugar { get; set; }
    public int Fiber { get; set; }
    public int Fat { get; set; }
    public int Protein { get; set; }
}