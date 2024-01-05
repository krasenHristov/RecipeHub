using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010110)]
public class CreateRecipeTable_SerialPrimaryKey : Migration
{
    public override void Up()
    {
        Execute.Sql("ALTER TABLE \"Recipe\" " +
                    "DROP COLUMN \"RecipeId\", " +
                    "DROP COLUMN \"ForkedFrom\", " +
                    "ADD COLUMN \"ForkedFromId\" INT DEFAULT NULL, " +
                    "ADD COLUMN \"RecipeId\" SERIAL PRIMARY KEY");
    }

    public override void Down()
    {
        Execute.Sql("ALTER TABLE \"Recipe\" " +
                    "DROP COLUMN \"RecipeId\" SERIAL PRIMARY KEY, " +
                    "DROP COLUMN \"ForkedFromId\", " +
                    "ADD COLUMN \"ForkedFrom\" INT DEFAULT NULL, " +
                    "ADD COLUMN \"RecipeId\" INT NOT NULL");
    }
}
