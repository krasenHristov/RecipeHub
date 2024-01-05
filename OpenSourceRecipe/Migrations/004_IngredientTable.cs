using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010104)]
public class CreateIngredientTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE TABLE \"Ingredient\"" +
                    "(" +
                    "\"IngredientId\" INT NOT NULL," +
                    "\"IngredientName\" VARCHAR(255) NOT NULL," +
                    "\"Calories\" TEXT NOT NULL," +
                    "\"Carbohydrate\" TEXT NOT NULL," +
                    "\"Sugar\" TEXT NOT NULL," +
                    "\"Fiber\" TEXT NOT NULL," +
                    "\"Fat\" TEXT NOT NULL," +
                    "\"Protein\" TEXT NOT NULL," +

                    "PRIMARY KEY (\"IngredientId\", \"IngredientName\")" +
                    ");");
    }

    public override void Down()
    {
        Delete.Table("Ingredient");
    }
}

