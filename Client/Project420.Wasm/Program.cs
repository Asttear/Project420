using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Project420.Client;
using Project420.Client.Services;

const string BedrockAddress = "https://localhost:7000/Bedrock/";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Main>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Services.AddMasaBlazor(builder =>
{
    builder.ConfigureTheme(theme =>
    {
        theme.Themes.Light.Primary = "#2196F3";   // blue
        theme.Themes.Light.Accent = "#82B1FF";    // blue accent-1
        theme.Themes.Light.Error = "#B71C1C";     // red darken-4
        theme.Themes.Light.Success = "#388E3C";   // green darken-2
        theme.Themes.Light.Warning = "#FFA726";   // orange lighten-1
        theme.Themes.Light.Info = "#03A9F4";      // light-blue
    });
}).AddI18nForWasmAsync($"{builder.HostEnvironment.BaseAddress}/_content/Project420.Client/i18n");

builder.Services.AddScoped(typeof(IApiService), typeof(ApiService));
builder.Services.AddHttpClient("PublicApi", client => client.BaseAddress = new(BedrockAddress));
builder.Services.AddHttpClient("PrivateApi", client => client.BaseAddress = new(BedrockAddress))
    .AddHttpMessageHandler(sp =>
    {
        var handler = sp.GetRequiredService<AuthorizationMessageHandler>()
            .ConfigureHandler(authorizedUrls: new[] { BedrockAddress });
        return handler;
    });

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

await builder.Build().RunAsync();
