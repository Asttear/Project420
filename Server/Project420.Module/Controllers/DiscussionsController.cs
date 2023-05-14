using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Contents;
using OrchardCore.Users.Services;
using Project420.Shared.Models;
using Project420.Module.Extensions;
using YesSql;

namespace Project420.Module.Controllers;

[Route("Api/Discussions")]
[ApiController]
[Authorize(AuthenticationSchemes = "Api"), IgnoreAntiforgeryToken, AllowAnonymous]
public class DiscussionsController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IContentManager _contentManager;
    private readonly ISession _session;
    private readonly IUserService _userService;

    private const string DiscussionItem = "Discussion";

    public DiscussionsController(IAuthorizationService authorizationService,
                                         IContentManager contentManager,
                                         ISession session,
                                         IUserService userService)
    {
        _authorizationService = authorizationService;
        _contentManager = contentManager;
        _session = session;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(int limit = 10, int offset = 0)
    {
        var discussion = await _contentManager.NewAsync(DiscussionItem);
        if (!await _authorizationService.AuthorizeAsync(User, CommonPermissions.ViewContent, discussion))
        {
            return Unauthorized(new { result = "权限不足。" });
        }

        IEnumerable<ContentItem> discussions = await _session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentType == DiscussionItem && index.Published)
            .ListAsync();
        await _contentManager.LoadAsync(discussions);
        var resultTasks = discussions
            .Skip(offset)
            .Take(limit)
            .Select(async d => new DiscussionMetadata()
            {
                Id = d.ContentItemId,
                Title = d.Content.TitlePart.Title,
                Author = (await _userService.GetUserProfileAsync(d.Author)).Nickname ?? d.Author,
                CreatedTime = d.CreatedUtc.HasValue ? DateTime.SpecifyKind(d.CreatedUtc.Value, DateTimeKind.Utc) : null
            });
        return Ok(await Task.WhenAll(resultTasks));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        var discussion = await _contentManager.NewAsync(DiscussionItem);
        if (!await _authorizationService.AuthorizeAsync(User, CommonPermissions.ViewContent, discussion))
        {
            return Unauthorized(new { result = "权限不足。" });
        }

        IEnumerable<ContentItem> discussions = await _session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentItemId == id && index.Published)
            .ListAsync();
        await _contentManager.LoadAsync(discussions);
        discussion = discussions.FirstOrDefault();
        if (discussion == null)
        {
            return NotFound();
        }
        ProfileModel profile = await _userService.GetUserProfileAsync(discussion.Author);
        DiscussionModel result = new()
        {
            Title = discussion.Content.TitlePart.Title,
            Author = profile.Nickname ?? discussion.Author,
            Gender = profile.Gender,
            Age = profile.Birthday.HasValue ? DateTimeOffset.Now.Year - profile.Birthday.Value.Year : null,
            HtmlContent = discussion.Content.Discussion.Content.Html,
            CreatedTime = discussion.CreatedUtc.HasValue ? DateTime.SpecifyKind(discussion.CreatedUtc.Value, DateTimeKind.Utc) : null
        };
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync()
    {
        var discussion = await _contentManager.NewAsync(DiscussionItem);
        if (!await _authorizationService.AuthorizeAsync(User, CommonPermissions.PublishOwnContent, discussion))
        {
            return Unauthorized(new { result = "权限不足。" });
        }

        StreamReader reader = new(Request.Body);
        var data = JsonConvert.DeserializeAnonymousType(await reader.ReadToEndAsync(), new { Title = "", HtmlContent = "" });

        discussion.Alter<ContentPart>("Discussion", part =>
        {
            part.ContentItem.DisplayText = data?.Title;
            part.ContentItem.Content.TitlePart.Title = data?.Title;
            part.Content.Content.Html = data?.HtmlContent;
        });

        await _contentManager.CreateAsync(discussion);
        await _contentManager.UpdateAsync(discussion);

        return Ok();
    }
}
