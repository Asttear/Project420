namespace Project420.Shared.Models;

public record DiscussionModel
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public Gender? Gender { get; set; }
    public int? Age { get; set; }
    public DateTimeOffset? CreatedTime { get; set; }
    public string? HtmlContent { get; set; }
}
