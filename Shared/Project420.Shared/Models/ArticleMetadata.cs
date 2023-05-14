namespace Project420.Shared.Models;

public record ArticleMetadata
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Source { get; set; }
    public DateTimeOffset? CreatedTime { get; set; }
};
