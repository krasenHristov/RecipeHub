using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010102)]
public class CreateCuisineTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE TABLE \"Cuisine\"" +
                    "(" +
                    "\"CuisineId\" SERIAL PRIMARY KEY," +
                    "\"CuisineName\" VARCHAR(255) NOT NULL," +
                    "\"Description\" TEXT NOT NULL," +
                    "\"CuisineImg\" VARCHAR(255) NOT NULL" +
                    ")");
        Execute.Sql("INSERT INTO \"Cuisine\" " +
                    "(\"CuisineName\",\"Description\", \"CuisineImg\") " +
                    "VALUES ('Example Cuisine', 'Example Cuisine Description', 'Example Cuisine IMG')");
    }

    public override void Down()
    {
        Delete.Table("Cuisine");
    }
}
