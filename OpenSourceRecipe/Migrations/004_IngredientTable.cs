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
                    "\"Nutrition\" TEXT NOT NULL," +

                    "PRIMARY KEY (\"IngredientId\", \"IngredientName\")" +
                    ");");
    }

    public override void Down()
    {
        Delete.Table("Ingredient");
    }
}

[Migration(2024010410)]

public class UpdateIngredientTable : Migration
{
    public override void Up()
    {
        Execute.Sql("ALTER TABLE \"Ingredient\" " +
                    "DROP COLUMN \"Nutrition\" " +
                    "ADD COLUMN \"Calories\" TEXT NOT NULL" +
                    "ADD COLUMN \"Carbohydrate\" TEXT NOT NULL" +
                    "ADD COLUMN \"Sugar\" TEXT NOT NULL" +
                    "ADD COLUMN \"Fiber\" TEXT NOT NULL" +
                    "ADD COLUMN \"Fat\" TEXT NOT NULL" +
                    "ADD COLUMN \"Protein\" TEXT NOT NULL;");
    }

    public override void Down()
    {
        Delete.Table("Ingredient");
    }
}

