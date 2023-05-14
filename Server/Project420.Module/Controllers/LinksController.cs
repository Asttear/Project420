using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Contents;
using Project420.Shared.Models;
using YesSql;

namespace Project420.Module.Controllers;

[Route("Api/Links")]
[ApiController]
[Authorize(AuthenticationSchemes = "Api"), IgnoreAntiforgeryToken, AllowAnonymous]
public class LinksController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IContentManager _contentManager;
    private readonly ISession _session;

    private const string LinkItem = "Link";

    public LinksController(IAuthorizationService authorizationService,
                                     IContentManager contentManager,
                                     ISession session)
    {
        _authorizationService = authorizationService;
        _contentManager = contentManager;
        _session = session;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var link = await _contentManager.NewAsync(LinkItem);
        if (!await _authorizationService.AuthorizeAsync(User, CommonPermissions.ViewContent, link))
        {
            return Unauthorized(new { result = "权限不足。" });
        }

        IEnumerable<ContentItem> links = await _session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentType == LinkItem && index.Published)
            .ListAsync();
        await _contentManager.LoadAsync(links);
        var result = links
            .Select(l => new LinkModel()
            {
                Title = l.Content.TitlePart.Title,
                Url = l.Content.Link.Url.Text,
                IconUrl = l.Content.Link.IconUrl.Text,
                Subtitle = l.Content.Link.Subtitle.Text
            }) ;
        return Ok(result);
    }
}
