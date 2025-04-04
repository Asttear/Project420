using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using Project420.Client.Services;

namespace Project420.Maui;

public static class MauiProgram
{
    const string BedrockAddress = "https://localhost:7000/Bedrock/";

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
               .ConfigureFonts(fonts =>
               {
                   fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
               });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services.AddMasaBlazor(builder =>
        {
            builder.ConfigureTheme(theme =>
            {
                theme.Themes.Light.Primary = "#2196F3";   // blue
                theme.Themes.Light.Accent = "#82B1FF";    // blue accent-1
                theme.Themes.Light.Error = "#B71C1C";     // red darken-4
                theme.Themes.Light.Success = "#388E3C";   // green darken-2
                theme.Themes.Light.Warning = "#FFA726";   // orange lighten-1
                theme.Themes.Light.Info = "#03A9F4";      // light-blue
            });
        }).AddI18nForMauiBlazor("wwwroot/i18n");

        builder.Services.AddMauiOidcAuthentication(options =>
        {
            var providerOptions = options.ProviderOptions;
            providerOptions.Authority = BedrockAddress;
            providerOptions.MetadataUrl = $"{BedrockAddress}/.well-known/openid-configuration";
            providerOptions.ClientId = "project420-client-dev";
            providerOptions.RedirectUri = "project420://Features/Authentication/Login-Callback";
            providerOptions.PostLogoutRedirectUri = "project420://Features/Authentication/Logout-Callback";
            providerOptions.ResponseType = "code";
            providerOptions.AdditionalProviderParameters["UsePkce"] = "true";
        }, builder => builder.PrimaryHandler = GetPlatformMessageHandler());

        builder.Services.AddScoped<IApiService, ApiService>();
        builder.Services.AddHttpClient("PublicApi", client => client.BaseAddress = new(BedrockAddress))
            .ConfigurePrimaryHttpMessageHandler(GetPlatformMessageHandler);
        builder.Services.AddHttpClient("PrivateApi", client => client.BaseAddress = new(BedrockAddress))
            .ConfigurePrimaryHttpMessageHandler(GetPlatformMessageHandler)
            .AddHttpMessageHandler(sp =>
            {
                var handler = sp.GetRequiredService<AuthorizationMessageHandler>()
                    .ConfigureHandler(authorizedUrls: [BedrockAddress]);
                return handler;
            });

        return builder.Build();
    }

    public static HttpMessageHandler GetPlatformMessageHandler()
    {
#if ANDROID
        var handler = new Xamarin.Android.Net.AndroidMessageHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=legion"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            }
        };
        return handler;
#elif IOS
        var handler = new NSUrlSessionHandler
        {
            TrustOverrideForUrl = (sender, url, trust) =>
            {
                if (url.StartsWith("https://localhost"))
                    return true;
                return false;
            };
        }
        return handler;
#else
        return new HttpClientHandler();
#endif
    }
}