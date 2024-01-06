using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010104)]
public class CreateIngredientTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE TABLE \"Ingredient\"" +
                    "(" +
                    "\"IngredientId\" SERIAL PRIMARY KEY," +
                    "\"IngredientName\" VARCHAR(255) NOT NULL UNIQUE," +
                    "\"Calories\" VARCHAR NOT NULL," +
                    "\"Carbohydrate\" VARCHAR NOT NULL," +
                    "\"Sugar\" VARCHAR NOT NULL," +
                    "\"Fiber\" VARCHAR NOT NULL," +
                    "\"Fat\" VARCHAR NOT NULL," +
                    "\"Protein\" VARCHAR NOT NULL" +
                    ");");
    }

    public override void Down()
    {
        Execute.Sql("DROP TABLE \"Ingredient\"");
    }
}

