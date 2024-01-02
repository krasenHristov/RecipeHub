using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010103)]
public class CreateRecipeTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE TABLE \"Recipe\"" +
                    "(" +
                    "\"RecipeId\" INT NOT NULL PRIMARY KEY," +
                    "\"RecipeTitle\" VARCHAR(255) NOT NULL," +
                    "\"TagLine\" VARCHAR(255) NOT NULL," +
                    "\"Difficulty\" INT NOT NULL," +
                    "\"TimeToPrepare\" INT NOT NULL," +
                    "\"RecipeMethod\" TEXT NOT NULL," +
                    "\"PostedOn\" DATE NOT NULL DEFAULT CURRENT_TIMESTAMP," +
                    "\"Cuisine\" TEXT NOT NULL," +
                    "\"RecipeImg\" VARCHAR(255) NOT NULL," +

                    "\"ForkedFrom\" INT DEFAULT NULL," +
                    "\"OriginalRecipeId\" INT DEFAULT NULL," +

                    "\"UserId\" INT NOT NULL REFERENCES \"User\" (\"UserId\")," +
                    "\"CuisineId\" INT NOT NULL REFERENCES \"Cuisine\" (\"CuisineId\")" +
                    ");");
    }

    public override void Down()
    {

        Delete.Table("Recipe");
    }
}
