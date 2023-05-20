using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;
using OrchardCore.Users.Services;
using Project420.Shared.Models;
using System.Security.Claims;
using OrchardUser = OrchardCore.Users.Models.User;

namespace Project420.Module.Extensions;

internal static class UserServiceExtensions
{
    private const string ProfileItem = "UserProfile";


    public static async Task<ProfileModel> GetUserProfileAsync(this IUserService userService, string userName)
    {
        if (await userService.GetUserAsync(userName) is not OrchardUser user)
        {
            throw new ArgumentException("User with provided user name does not exist.");
        }
        return GetUserProfile(user);
    }

    public static async Task<ProfileModel> GetUserProfileAsync(this IUserService userService, ClaimsPrincipal userClaims)
    {
        if (await userService.GetAuthenticatedUserAsync(userClaims) is not OrchardUser user)
        {
            throw new ArgumentException("User is invalid.");
        }
        return GetUserProfile(user);
    }

    private static ProfileModel GetUserProfile(OrchardUser user)
    {
        if (user.Properties[ProfileItem] is not JToken jToken)
        {
            return new ProfileModel();
        }
        if (jToken.ToObject<ContentItem>() is not ContentItem profileItem)
        {
            throw new ArgumentException("User is invalid.");
        }
        string? birthday = (string)profileItem.Content.UserProfile.Birthday.Value;
        ProfileModel profile = new()
        {
            Nickname = profileItem.Content.UserProfile.Nickname.Text,
            Gender = Enum.Parse<Gender>((string)profileItem.Content.UserProfile.Gender.Text),
            Birthday = birthday is null ? null : DateOnly.FromDateTime(DateTime.Parse(birthday))
        };
        return profile;
    }
}
