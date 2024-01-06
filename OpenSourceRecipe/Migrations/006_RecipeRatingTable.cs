using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010106)]
public class CreateRecipeRatingTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE TABLE \"RecipeRating\"" +
                    "(" +
                    "\"RecipeId\" INT NOT NULL REFERENCES \"Recipe\" (\"RecipeId\")," +
                    "\"UserId\" INT NOT NULL REFERENCES \"User\" (\"UserId\")," +
                    "\"RATING\" INT NOT NULL" +
                    ");"
                    );
    }

    public override void Down()
    {
        Execute.Sql("DROP TABLE \"RecipeRating\"");
    }
}
