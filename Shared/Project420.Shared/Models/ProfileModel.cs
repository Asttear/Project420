namespace Project420.Shared.Models;

public record ProfileModel
{
    public string? Nickname { get; set; }
    public Gender? Gender { get; set; }
    public DateOnly? Birthday { get; set; }
}

public enum Gender
{
    Confidential,
    Male,
    Female
}
