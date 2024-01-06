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
                    "\"ProfileImg\" VARCHAR(500) NOT NULL DEFAULT 'https://www.outsystems.com/Forge_CW/_image.aspx/Q8LvY--6WakOw9afDCuuGdL9c3WA3ttAt5pfSB[…]-upload-example-2023-01-04%2000-00-00-2023-07-24%2020-02-59'," +
                    "\"Status\" BOOLEAN NOT NULL," +
                    "\"Bio\" TEXT NOT NULL" +
                    ")");
    }

    public override void Down()
    {
        Execute.Sql("DROP TABLE \"User\"");
    }
}
