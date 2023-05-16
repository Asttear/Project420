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

[Route("Api/Cases")]
[ApiController]
[Authorize(AuthenticationSchemes = "Api"), IgnoreAntiforgeryToken, AllowAnonymous]
public class CasesController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IContentManager _contentManager;
    private readonly ISession _session;
    private readonly IUserService _userService;

    private const string CaseTypeName = "Case";

    public CasesController(IAuthorizationService authorizationService,
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
        var caseItem = await _contentManager.NewAsync(CaseTypeName);
        if (!await _authorizationService.AuthorizeAsync(User, CommonPermissions.ViewContent, caseItem))
        {
            return Unauthorized(new { result = "权限不足。" });
        }

        IEnumerable<ContentItem> cases = await _session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentType == CaseTypeName && index.Published)
            .ListAsync();
        await _contentManager.LoadAsync(cases);
        var resultTasks = cases
            .Skip(offset)
            .Take(limit)
            .Select(async c => new CaseMetadata()
            {
                Id = c.ContentItemId,
                Author = (await _userService.GetUserProfileAsync(c.Author)).Nickname ?? c.Author,
                CreatedTime = c.CreatedUtc.HasValue ? DateTime.SpecifyKind(c.CreatedUtc.Value, DateTimeKind.Utc) : null,
                Date = DateTimeOffset.Parse((string)c.Content.Case.Date.Value),
                Gender = Enum.Parse<Gender>((string)c.Content.Case.Gender.Text),
                Age = c.Content.Case.Age.Value
            });
        return Ok(await Task.WhenAll(resultTasks));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        var caseItem = await _contentManager.NewAsync(CaseTypeName);
        if (!await _authorizationService.AuthorizeAsync(User, CommonPermissions.ViewContent, caseItem))
        {
            return Unauthorized(new { result = "权限不足。" });
        }

        IEnumerable<ContentItem> cases = await _session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentItemId == id && index.Published)
            .ListAsync();
        await _contentManager.LoadAsync(cases);
        caseItem = cases.FirstOrDefault();
        if (caseItem == null)
        {
            return NotFound();
        }
        CaseModel result = new()
        {
            Author = (await _userService.GetUserProfileAsync(caseItem.Author)).Nickname ?? caseItem.Author,
            CreatedTime = caseItem.CreatedUtc.HasValue ? DateTime.SpecifyKind(caseItem.CreatedUtc.Value, DateTimeKind.Utc) : null,
            Date = DateTimeOffset.Parse((string)caseItem.Content.Case.Date.Value),
            Gender = Enum.Parse<Gender>((string)caseItem.Content.Case.Gender.Text),
            Age = caseItem.Content.Case.Age.Value,
            Symptoms = caseItem.Content.Case.Symptoms.Text,
            Treatment = caseItem.Content.Case.Treatment.Text,
        };
        return Ok(result);
    }
}
