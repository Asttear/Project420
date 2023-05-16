using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Contents;
using OrchardCore.Shortcodes.Services;
using Project420.Shared.Models;
using YesSql;

namespace Project420.Module.Controllers;

[Route("Api/Articles")]
[ApiController]
[Authorize(AuthenticationSchemes = "Api"), IgnoreAntiforgeryToken, AllowAnonymous]
public class ArticlesController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IContentManager _contentManager;
    private readonly ISession _session;
    private readonly IShortcodeService _shortcode;

    private const string ArticleItem = "Article";

    public ArticlesController(IAuthorizationService authorizationService,
                                     IContentManager contentManager,
                                     ISession session,
                                     IShortcodeService shortcode)
    {
        _authorizationService = authorizationService;
        _contentManager = contentManager;
        _session = session;
        _shortcode = shortcode;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(int limit = 10, int offset = 0)
    {
        var article = await _contentManager.NewAsync(ArticleItem);
        if (!await _authorizationService.AuthorizeAsync(User, CommonPermissions.ViewContent, article))
        {
            return Unauthorized(new { result = "权限不足。" });
        }

        IEnumerable<ContentItem> articles = await _session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentType == ArticleItem && index.Published)
            .ListAsync();
        await _contentManager.LoadAsync(articles);
        var result = articles
            .Skip(offset)
            .Take(limit)
            .Select(a => new ArticleMetadata()
            {
                Id = a.ContentItemId,
                Title = a.Content.TitlePart.Title,
                Source = a.Content.Article.Source.Text,
                CreatedTime = a.CreatedUtc.HasValue ? DateTime.SpecifyKind(a.CreatedUtc.Value, DateTimeKind.Utc) : null
            });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        var article = await _contentManager.NewAsync(ArticleItem);
        if (!await _authorizationService.AuthorizeAsync(User, CommonPermissions.ViewContent, article))
        {
            return Unauthorized(new { result = "权限不足。" });
        }

        IEnumerable<ContentItem> articles = await _session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentItemId == id && index.Published)
            .ListAsync();
        await _contentManager.LoadAsync(articles);
        article = articles.FirstOrDefault();
        if (article == null)
        {
            return NotFound();
        }
        ArticleModel result = new()
        {
            Title = article.Content.TitlePart.Title,
            Source = article.Content.Article.Source.Text,
            HtmlContent = await _shortcode.ProcessAsync((string)article.Content.Article.Content.Html),
            CreatedTime = article.CreatedUtc.HasValue ? DateTime.SpecifyKind(article.CreatedUtc.Value, DateTimeKind.Utc) : null
        };
        return Ok(result);
    }
}
