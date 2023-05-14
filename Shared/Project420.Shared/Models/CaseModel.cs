namespace Project420.Shared.Models;

public record CaseModel
{
    public string? Author { get; set; }
    public DateTimeOffset? CreatedTime { get; set; }
    public DateTimeOffset? Date { get; set; }
    public Gender? Gender { get; set; }
    public int? Age { get; set; }
    public string? Symptoms { get; set; }
    public string? Treatment { get; set; }
}