using Project420.Shared.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Project420.Client.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _publicHttp;
    private readonly HttpClient _privateHttp;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ApiService(IHttpClientFactory httpClientFactory)
    {
        _publicHttp = httpClientFactory.CreateClient("PublicApi");
        _privateHttp = httpClientFactory.CreateClient("PrivateApi");
    }

    public async Task<IList<CommentModel>?> GetCommentsAsync(string id, int count)
    {
        var comments = await _publicHttp.GetFromJsonAsync<IList<CommentModel>>(
            $"Api/Comments?articleId={id}&offset={count}",
            _jsonOptions
        );
        if (comments?.Count > 0)
        {
            return comments;
        }
        return null;
    }

    public async Task AddCommentAsync(string articleId, string comment)
    {
        StringContent content = new(comment);
        var response = await _privateHttp.PostAsync($"Api/Comments?articleId={articleId}", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("请求失败。", null, response.StatusCode);
        }
    }

    public async Task<IList<ArticleMetadata>?> ListArticlesAsync(int count)
    {
        var articles = await _publicHttp.GetFromJsonAsync<IList<ArticleMetadata>>($"Api/Articles?offset={count}");
        if (articles?.Count > 0)
        {
            return articles;
        }
        return null;
    }

    public async Task<ArticleModel> GetArticleAsync(string id)
    {
        var article = await _publicHttp.GetFromJsonAsync<ArticleModel>($"Api/Articles/{id}");
        return article ?? throw new ArgumentException("Cannot get the article matches this id.");
    }

    public async Task<IList<CaseMetadata>?> ListCasesAsync(int count)
    {
        var cases = await _publicHttp.GetFromJsonAsync<IList<CaseMetadata>>($"Api/Cases?offset={count}");
        if (cases?.Count > 0)
        {
            return cases;
        }
        return null;
    }

    public async Task<CaseModel> GetCaseAsync(string id)
    {
        var caseItem = await _publicHttp.GetFromJsonAsync<CaseModel>($"Api/Cases/{id}");
        return caseItem ?? throw new ArgumentException("Cannot get the case matches this id.");
    }

    public async Task<IList<DiscussionMetadata>?> ListDiscussionsAsync(int count)
    {
        var discussions = await _publicHttp.GetFromJsonAsync<IList<DiscussionMetadata>>($"Api/Discussions?offset={count}");
        if (discussions?.Count > 0)
        {
            return discussions;
        }
        return null;
    }

    public async Task<DiscussionModel> GetDiscussionAsync(string id)
    {
        var discussion = await _publicHttp.GetFromJsonAsync<DiscussionModel>($"Api/Discussions/{id}");
        return discussion ?? throw new ArgumentException("Cannot get the discussion matches this id.");
    }

    public async Task AddDiscussionAsync(string title, string htmlContent)
    {
        var response = await _privateHttp.PostAsJsonAsync("Api/Discussions", new { title, htmlContent });
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("请求失败。", null, response.StatusCode);
        }
    }

    public async Task<ProfileModel> GetProfileAsync()
    {
        var profile = await _privateHttp.GetFromJsonAsync<ProfileModel>("Api/Users");
        return profile ?? throw new HttpRequestException();
    }

    public async Task<ProfileModel> GetProfileAsync(string userName)
    {
        var profile = await _publicHttp.GetFromJsonAsync<ProfileModel>($"Api/Users/{userName}");
        return profile ?? throw new HttpRequestException();
    }

    public async Task<ProfileModel> ModifyProfileAsync(ProfileModel profile)
    {
        var response = await _privateHttp.PutAsJsonAsync("Api/Users", profile);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("请求失败。", null, response.StatusCode);
        }
        return await JsonSerializer.DeserializeAsync<ProfileModel>(response.Content.ReadAsStream()) ?? throw new Exception();
    }

    public async Task<IList<LinkModel>> GetLinksAsync()
    {
        var links = await _publicHttp.GetFromJsonAsync<IList<LinkModel>>(
            "Api/Links",
            _jsonOptions
        );
        return links ?? new List<LinkModel>();
    }
}
