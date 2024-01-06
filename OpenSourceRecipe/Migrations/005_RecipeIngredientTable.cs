using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010105)]
public class CreateRecipeIngredientTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE TABLE \"RecipeIngredient\"" +
                    "(" +
                    "\"Quantity\" TEXT NOT NULL," +
                    "\"RecipeId\" INT NOT NULL REFERENCES \"Recipe\" (\"RecipeId\")," +
                    "\"IngredientId\" INT NOT NULL REFERENCES \"Ingredient\" (\"IngredientId\")," +
                    "\"IngredientName\" VARCHAR(255) NOT NULL" +
                    ");"
        );
    }

    public override void Down()
    {
        Execute.Sql("DROP TABLE \"RecipeIngredient\"");
    }
}
