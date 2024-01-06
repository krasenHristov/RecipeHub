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
                    "\"RecipeId\" INT NOT NULL REFERENCES \"Recipe\" (\"RecipeId\") ON DELETE CASCADE," +
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

[Migration(2024010105_01)]
public class DropIngredientNameColumn : Migration
{
    public override void Up()
    {
        Execute.Sql("ALTER TABLE \"RecipeIngredient\" DROP COLUMN \"IngredientName\";");
    }

    public override void Down()
    {
        // no need to do anything here as the column will be added back in the next migration
    }

}
