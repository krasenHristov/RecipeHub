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
        Execute.Sql("DROP TABLE \"CommentVote\"");
    }
}

// add on delete cascade to commentvote table migration
[Migration(2024010108_01)]
public class AddOnDeleteCascadeToCommentVoteTable : Migration
{
    public override void Up()
    {
        Execute.Sql("ALTER TABLE \"CommentVote\" " +
                    "DROP CONSTRAINT \"CommentVote_UserId_fkey\" CASCADE;" +
                    "ALTER TABLE \"CommentVote\" " +
                    "DROP CONSTRAINT \"CommentVote_CommentId_fkey\" CASCADE;" +
                    "ALTER TABLE \"CommentVote\" " +
                    "ADD CONSTRAINT \"CommentVote_UserId_fkey\" " +
                    "FOREIGN KEY (\"UserId\") " +
                    "REFERENCES \"User\" (\"UserId\") " +
                    "ON DELETE CASCADE;" +
                    "ALTER TABLE \"CommentVote\" " +
                    "ADD CONSTRAINT \"CommentVote_CommentId_fkey\" " +
                    "FOREIGN KEY (\"CommentId\") " +
                    "REFERENCES \"RecipeComment\" (\"CommentId\") " +
                    "ON DELETE CASCADE;"
                    );
    }

    public override void Down()
    {
        Execute.Sql("ALTER TABLE \"CommentVote\" " +
                    "DROP CONSTRAINT \"CommentVote_UserId_fkey\" CASCADE;" +
                    "ALTER TABLE \"CommentVote\" " +
                    "DROP CONSTRAINT \"CommentVote_CommentId_fkey\" CASCADE;" +
                    "ALTER TABLE \"CommentVote\" " +
                    "ADD CONSTRAINT \"CommentVote_UserId_fkey\" " +
                    "FOREIGN KEY (\"UserId\") " +
                    "REFERENCES \"User\" (\"UserId\");" +
                    "ALTER TABLE \"CommentVote\" " +
                    "ADD CONSTRAINT \"CommentVote_CommentId_fkey\" " +
                    "FOREIGN KEY (\"CommentId\") " +
                    "REFERENCES \"RecipeComment\" (\"CommentId\");"
                    );
    }
}
