using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OpenSourceRecipes.Models;

public class Cuisine
{
    // Primary Key
    public int CuisineId {get; set; }
    // Foreign Key to the Recipe table
    [MaxLength(255)]
    public string? CuisineName { get; set; }
    public string? CuisineImg { get; set; }
    public string? CuisineDescription { get; set; }
}
