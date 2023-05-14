namespace Project420.Shared.Models;

public record ArticleModel
{
    public string? Title { get; set; }
    public string? Source { get; set; }
    public DateTimeOffset? CreatedTime { get; set; }
    public string? HtmlContent { get; set; }
}
