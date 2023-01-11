using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Project420.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

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
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
