namespace OpenSourceRecipes.Models;

public class CommentVote
{
    // Primary Key
    public int CommentVoteId { get; set; }

    // Foreign Key to the Comment table
    public int CommentId { get; set; }
    // Foreign Key to the Comment table
    public int UserId { get; set; }

    // True if the user upvoted the comment, false if they downvoted
    public bool UpVote { get; set; }
}
