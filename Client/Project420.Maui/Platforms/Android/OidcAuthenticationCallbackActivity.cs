using Android.App;
using Android.Content;
using Android.Content.PM;

namespace Project420.Maui.Platforms.Android;

[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
[IntentFilter(new[] { Intent.ActionView },
          Categories = new[] {
            Intent.CategoryDefault,
            Intent.CategoryBrowsable
          },
          DataScheme = CALLBACK_SCHEME,
          DataPaths = new[] {
              "Features/Authentication/Login-Callback",
              "Features/Authentication/Logout-Callback"
              })]
public class OidcAuthenticationCallbackActivity : WebAuthenticatorCallbackActivity
{
    const string CALLBACK_SCHEME = "project420";
}
