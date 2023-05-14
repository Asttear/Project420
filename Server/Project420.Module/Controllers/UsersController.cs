using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;
using OrchardCore.Users;
using OrchardCore.Users.Services;
using Project420.Shared.Models;
using Project420.Module.Extensions;
using YesSql;
using OrchardUser = OrchardCore.Users.Models.User;

namespace Project420.Module.Controllers;

[Route("Api/Users")]
[ApiController]
[Authorize(AuthenticationSchemes = "Api"), IgnoreAntiforgeryToken, AllowAnonymous]
public class UsersController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IContentManager _contentManager;
    private readonly UserManager<IUser> _userManager;
    private readonly IUserService _userService;
    private readonly ISession _session;

    public UsersController(IAuthorizationService authorizationService,
                                 IContentManager contentManager,
                                 UserManager<IUser> userManager,
                                 IUserService userService,
                                 ISession session)
    {
        _authorizationService = authorizationService;
        _contentManager = contentManager;
        _userManager = userManager;
        _userService = userService;
        _session = session;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _userService.GetUserProfileAsync(User));
    }

    [HttpGet("{userName}")]
    public async Task<IActionResult> GetAsync(string userName)
    {
        return Ok(await _userService.GetUserProfileAsync(userName));
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync()
    {
        if (await _userService.GetAuthenticatedUserAsync(User) is not OrchardUser user)
        {
            return BadRequest();
        }
        var jsonOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var profile = await JsonSerializer.DeserializeAsync<ProfileModel>(Request.Body, jsonOptions);
        if (profile is null)
        {
            return BadRequest();
        }
        await ModifyUserProfileAsync(user, profile);
        return Ok(await _userService.GetUserProfileAsync(User));
    }

    private async Task ModifyUserProfileAsync(OrchardUser user, ProfileModel profile)
    {
        if (user.Properties["UserProfile"] is not JToken jToken
            || jToken.ToObject<ContentItem>() is not ContentItem profileItem)
        {
            throw new ArgumentException("User is invalid.");
        }

        profileItem.Alter<ContentPart>("UserProfile", part =>
        {
            part.Content.Nickname.Text = profile.Nickname ?? "";
            part.Content.Gender.Text = profile.Gender?.ToString();
            part.Content.Birthday.Value = profile.Birthday?.ToString();
        });

        user.Properties["UserProfile"] = JObject.FromObject(profileItem);
        await _userManager.UpdateAsync(user);
    }
}
