namespace Project420.Shared.Models;

public record CaseMetadata
{
    public string? Id { get; set; }
    public string? Author { get; set; }
    public DateTimeOffset? CreatedTime { get; set; }
    public DateTimeOffset? Date { get; set; }
    public Gender? Gender { get; set; }
    public int? Age { get; set; }
}