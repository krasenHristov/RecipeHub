using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010103)]
public class CreateRecipeTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE TABLE \"Recipe\"" +
                    "(" +
                    "\"RecipeId\" SERIAL PRIMARY KEY," +
                    "\"RecipeTitle\" VARCHAR(255) NOT NULL," +
                    "\"TagLine\" VARCHAR(255) NOT NULL," +
                    "\"Difficulty\" INT NOT NULL," +
                    "\"TimeToPrepare\" INT NOT NULL," +
                    "\"RecipeMethod\" TEXT NOT NULL," +
                    "\"PostedOn\" DATE NOT NULL DEFAULT CURRENT_TIMESTAMP," +
                    "\"Cuisine\" TEXT NOT NULL," +
                    "\"RecipeImg\" VARCHAR(255) NOT NULL," +

                    "\"ForkedFromId\" INT DEFAULT NULL," +
                    "\"OriginalRecipeId\" INT DEFAULT NULL," +

                    "\"UserId\" INT REFERENCES \"User\" (\"UserId\") ON DELETE SET NULL," +
                    "\"CuisineId\" INT NOT NULL REFERENCES \"Cuisine\" (\"CuisineId\") ON DELETE CASCADE" +
                    ");");
    }

    public override void Down()
    {
        Execute.Sql("DROP TABLE \"Recipe\"");
    }
}

[Migration(2024010103_01)]
public class FullTextSearchRecipeTable : Migration
{
    public override void Up()
    {
        Execute.Sql("ALTER TABLE \"Recipe\" ADD COLUMN \"TsvDescription\" TSVECTOR;");
        Execute.Sql("UPDATE \"Recipe\" SET \"TsvDescription\" = to_tsvector('simple', \"RecipeTitle\" || ' ' || \"RecipeMethod\" || ' ' || \"Cuisine\");");
        Execute.Sql("CREATE INDEX \"Recipe_FTS_Simple\" ON \"Recipe\" USING GIN (\"TsvDescription\")");
        Execute.Sql("CREATE TRIGGER \"Recipe_TsvDescription_update\" BEFORE INSERT OR UPDATE ON \"Recipe\" " +
                    "FOR EACH ROW EXECUTE PROCEDURE tsvector_update_trigger(\"TsvDescription\", 'pg_catalog.english', \"RecipeTitle\", \"RecipeMethod\", \"Cuisine\")");
    }

    public override void Down()
    {
    }
}
