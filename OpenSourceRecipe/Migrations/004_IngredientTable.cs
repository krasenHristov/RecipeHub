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
                    "\"IngredientName\" VARCHAR(255) NOT NULL," +
                    "\"Calories\" INT NOT NULL," +
                    "\"Carbohydrate\" INT NOT NULL," +
                    "\"Sugar\" INT NOT NULL," +
                    "\"Fiber\" INT NOT NULL," +
                    "\"Fat\" INT NOT NULL," +
                    "\"Protein\" INT NOT NULL" +
                    ");");
    }

    public override void Down()
    {
        Delete.Table("Ingredient");
    }
}

