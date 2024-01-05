/*
using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010109)]
public class CreateFavouritesTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE TABLE \"Favourites\"" +
                    "(" +
                    "\"UserId\" INT NOT NULL REFERENCES \"User\" (\"UserId\")," +
                    "\"RecipeId\" INT NOT NULL REFERENCES \"Recipe\" (\"RecipeId\")" +
                    ");"
                    );
    }

    public override void Down()
    {
        Delete.Table("Favourites");
    }
}
*/
