using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010107)]
public class CreateRecipeCommentTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE TABLE \"RecipeComment\"" +
                    "(" +
                    "\"CommentId\" INT NOT NULL PRIMARY KEY," +
                    "\"Comment\" TEXT NOT NULL," +
                    "\"PostedOn\" DATE NOT NULL DEFAULT CURRENT_TIMESTAMP," +
                    "\"UserId\" INT NOT NULL REFERENCES \"User\" (\"UserId\")," +
                    "\"RecipeId\" INT NOT NULL REFERENCES \"Recipe\" (\"RecipeId\")" +
                    ");"
                    );
    }

    public override void Down()
    {
        Execute.Sql("DROP TABLE \"RecipeComment\"");
    }
}
