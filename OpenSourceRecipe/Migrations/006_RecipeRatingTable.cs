using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010106)]
public class CreateRecipeRatingTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE TABLE \"RecipeRating\"" +
                    "(" +
                    "\"RecipeId\" INT NOT NULL REFERENCES \"Recipe\" (\"RecipeId\") ON DELETE CASCADE," +
                    "\"UserId\" INT NOT NULL REFERENCES \"User\" (\"UserId\") ON DELETE CASCADE," +
                    "\"RATING\" INT NOT NULL" +
                    ");"
                    );
    }


    public override void Down()
    {
        Execute.Sql("DROP TABLE \"RecipeRating\"");
    }
}

[Migration(2024010106_01)]
public class AddUniqueConstraintToRecipeRating : Migration
{
    public override void Up()
    {
        Execute.Sql("ALTER TABLE \"RecipeRating\" ADD CONSTRAINT \"UniqueRecipeUser\" UNIQUE (\"RecipeId\", \"UserId\");");
    }

    public override void Down()
    {
        Execute.Sql("ALTER TABLE \"RecipeRating\" DROP CONSTRAINT \"UniqueRecipeUser\";");
    }
}

