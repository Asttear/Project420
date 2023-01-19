namespace Project420.Client.Models;

public class Post
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string HtmlContent { get; set; } = string.Empty;
    public string[] Tags { get; set; } = Array.Empty<string>();
    public int LikeCount { get; set; } = 0;
}
