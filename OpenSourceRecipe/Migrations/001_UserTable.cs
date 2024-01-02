using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010101)] // Version number as YYYYMMDDNN (Year, Month, Day, Iteration)
public class CreateUserTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE TABLE \"User\"" +
                    "(" +
                    "\"UserId\" SERIAL PRIMARY KEY," +
                    "\"Username\" VARCHAR(255) NOT NULL," +
                    "\"Name\" VARCHAR(255) NOT NULL," +
                    "\"Password\" VARCHAR(255) NOT NULL," +
                    "\"ProfileImg\" VARCHAR(255) NOT NULL," +
                    "\"Status\" BOOLEAN NOT NULL," +
                    "\"Bio\" TEXT NOT NULL" +
                    ")");
    }

    public override void Down()
    {
        Delete.Table("User");
    }
}
