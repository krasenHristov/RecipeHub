using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010107)]
public class CreateRecipeCommentTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE TABLE \"RecipeComment\"" +
                    "(" +
                    "\"CommentId\" SERIAL PRIMARY KEY," +
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

[Migration(2024010107_01)]
public class AddUserNameTable : Migration
{
    public override void Up()
    {
        Execute.Sql("ALTER TABLE \"RecipeComment\" ADD COLUMN \"Author\" VARCHAR(255) NOT NULL;");
        Execute.Sql("ALTER TABLE \"RecipeComment\" DROP COLUMN \"RecipeId\";");
        Execute.Sql("ALTER TABLE \"RecipeComment\" ADD COLUMN \"RecipeId\" INT NOT NULL REFERENCES \"Recipe\" (\"RecipeId\") ON DELETE CASCADE;");
    }

    public override void Down()
    {
    }
}
