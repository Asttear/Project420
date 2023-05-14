namespace Project420.Shared.Models;

public record DiscussionMetadata
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public DateTimeOffset? CreatedTime { get; set; }
};
