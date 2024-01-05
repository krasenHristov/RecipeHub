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

        Delete.Table("Recipe");
    }
}

/*
[Migration(2024010503_01)]
public class AlterRecipeTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE SEQUENCE recipe_id_seq;");
        Execute.Sql("ALTER TABLE \"Recipe\" ALTER COLUMN \"RecipeId\" SET DEFAULT nextval('recipe_id_seq')");
        Execute.Sql("ALTER TABLE \"Recipe\" ALTER COLUMN \"ForkedFrom\" DROP DEFAULT;");
        Execute.Sql("ALTER TABLE \"Recipe\" ADD COLUMN \"ForkedFromId\" INT DEFAULT NULL;");
    }

    public override void Down()
    {
        Execute.Sql("ALTER TABLE \"Recipe\" DROP COLUMN \"ForkedFromId\";");
        Execute.Sql("ALTER TABLE \"Recipe\" ALTER COLUMN \"ForkedFrom\" SET DEFAULT NULL;");
        Execute.Sql("ALTER TABLE \"Recipe\" ALTER COLUMN \"RecipeId\" SET DEFAULT NULL;");
        Execute.Sql("DROP SEQUENCE recipe_id_seq;");
    }
}

[Migration(2024010503_03)]
public class AlterForeignKeyInRecipeTable : Migration
{
    public override void Up()
    {
        Execute.Sql("ALTER TABLE \"Recipe\" ALTER COLUMN \"UserId\" DROP NOT NULL;");

        // drop the existing foreign key constraint
        Execute.Sql("ALTER TABLE \"Recipe\" DROP CONSTRAINT IF EXISTS \"Recipe_UserId_fkey\";");

        // add the foreign key constraint with ON DELETE SET NULL
        Execute.Sql("ALTER TABLE \"Recipe\" ADD CONSTRAINT \"Recipe_UserId_fkey\" FOREIGN KEY (\"UserId\") REFERENCES \"User\" (\"UserId\") ON DELETE SET NULL;");
    }

    public override void Down()
    {
        // drop the modified constraint
        Execute.Sql("ALTER TABLE \"Recipe\" DROP CONSTRAINT IF EXISTS \"Recipe_UserId_fkey\";");

        // add back the original constraint
        // replace with the original constraint definition
        Execute.Sql("ALTER TABLE \"Recipe\" ADD CONSTRAINT \"Recipe_UserId_fkey\" FOREIGN KEY (\"UserId\") REFERENCES \"User\" (\"UserId\");");
    }
}

[Migration(2024010503_04)]
public class UpdateCuisineForeignKey : Migration
{
    public override void Up()
    {
        Execute.Sql("ALTER TABLE \"Recipe\" DROP CONSTRAINT IF EXISTS \"Recipe_CuisineId_fkey\";");

        Execute.Sql("ALTER TABLE \"Recipe\" ADD CONSTRAINT \"Recipe_CuisineId_fkey\" FOREIGN KEY (\"CuisineId\") REFERENCES \"Cuisine\" (\"CuisineId\") ON DELETE CASCADE;");
    }

    public override void Down()
    {
        Execute.Sql("ALTER TABLE \"Recipe\" DROP CONSTRAINT IF EXISTS \"Recipe_CuisineId_fkey\";");

        Execute.Sql("ALTER TABLE \"Recipe\" ADD CONSTRAINT \"Recipe_CuisineId_fkey\" FOREIGN KEY (\"CuisineId\") REFERENCES \"Cuisine\" (\"CuisineId\");");
    }
}
*/
