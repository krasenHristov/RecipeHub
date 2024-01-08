using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010108)]
public class CreateCommentVoteTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE TABLE \"CommentVote\"" +
                    "(" +
                    "\"CommentId\" INT NOT NULL REFERENCES \"RecipeComment\" (\"CommentId\") ON DELETE CASCADE," +
                    "\"UserId\" INT NOT NULL REFERENCES \"User\" (\"UserId\") ON DELETE CASCADE," +
                    "\"Upvote\" BOOL NOT NULL" +
                    ");"
                    );
    }

    public override void Down()
    {
        Execute.Sql("DROP TABLE \"CommentVote\"");
    }
}

[Migration(2024010108_01)]
public class UpdateCommentVoteTable : Migration
{
    public override void Up()
    {
        Execute.Sql("ALTER TABLE \"CommentVote\" ADD CONSTRAINT \"UniqueCommentUser\" UNIQUE (\"CommentId\", \"UserId\");");
    }

    public override void Down()
    {
        Execute.Sql("ALTER TABLE \"CommentVote\" DROP CONSTRAINT \"UniqueCommentUser\";");
    }
}
