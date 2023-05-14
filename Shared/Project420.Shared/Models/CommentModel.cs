namespace Project420.Shared.Models;

public record CommentModel
{
    public string? Author { get; set; }

    public DateTimeOffset? CreatedTime { get; set; }

    public string? HtmlContent { get; set; }
}
