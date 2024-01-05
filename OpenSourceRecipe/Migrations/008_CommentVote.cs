using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010108)]
public class CreateCommentVoteTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE TABLE \"CommentVote\"" +
                    "(" +
                    "\"CommentId\" INT NOT NULL REFERENCES \"RecipeComment\" (\"CommentId\")," +
                    "\"UserId\" INT NOT NULL REFERENCES \"User\" (\"UserId\")," +
                    "\"Upvote\" BOOL NOT NULL" +
                    ");"
                    );
    }

    public override void Down()
    {
        Delete.Table("CommentVote");
    }
}
