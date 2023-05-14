using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Contents;
using OrchardCore.Users.Services;
using Project420.Module.Extensions;
using Project420.Shared.Models;
using YesSql;

namespace Project420.Module.Controllers;

[Route("Api/Comments")]
[ApiController]
[Authorize(AuthenticationSchemes = "Api"), IgnoreAntiforgeryToken, AllowAnonymous]
public class CommentsController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IContentManager _contentManager;
    private readonly ISession _session;
    private readonly IUserService _userService;

    private const string CommentItem = "Comment";

    public CommentsController(IAuthorizationService authorizationService,
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
    public async Task<IActionResult> GetAsync(string articleId, int limit = 10, int offset = 0)
    {
        var comment = await _contentManager.NewAsync(CommentItem);
        if (!await _authorizationService.AuthorizeAsync(User, CommonPermissions.ViewContent, comment))
        {
            return Unauthorized(new { result = "权限不足。" });
        }

        IEnumerable<ContentItem> comments = await _session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentType == CommentItem && index.Published)
            .ListAsync();
        await _contentManager.LoadAsync(comments);
        var resultTasks = comments
            .Where(c => c.Content.Comment.RelatedItem.Text == articleId)
            .Skip(offset)
            .Take(limit)
            .Select(async c => new CommentModel()
            {
                CreatedTime = c.CreatedUtc.HasValue ? DateTime.SpecifyKind(c.CreatedUtc.Value, DateTimeKind.Utc) : null,
                Author = (await _userService.GetUserProfileAsync(c.Author)).Nickname ?? c.Author,
                HtmlContent = c.Content.Comment.Content.Html
            });
        return Ok(await Task.WhenAll(resultTasks));
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(string articleId)
    {
        var comment = await _contentManager.NewAsync(CommentItem);
        if (!await _authorizationService.AuthorizeAsync(User, CommonPermissions.PublishOwnContent, comment))
        {
            return Unauthorized(new { result = "权限不足。" });
        }

        StreamReader reader = new(Request.Body);

        comment.Alter<ContentPart>("Comment", async part =>
        {
            part.Content.RelatedItem.Text = articleId;
            part.Content.Content.Html = await reader.ReadToEndAsync();
        });

        await _contentManager.CreateAsync(comment);
        await _contentManager.UpdateAsync(comment);

        return Ok();
    }
}
