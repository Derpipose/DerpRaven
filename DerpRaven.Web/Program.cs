using DerpRaven.Web.ApiClients;
using DerpRaven.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddLogging();

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.MetadataUrl = "https://engineering.snow.edu/auth/realms/SnowCollege/.well-known/openid-configuration";
    options.ProviderOptions.Authority = "https://engineering.snow.edu/auth/realms/SnowCollege";
    options.ProviderOptions.ClientId = "DerpRavenBlazorAuth";
    options.ProviderOptions.ResponseType = "id_token token";

    options.UserOptions.NameClaim = "preferred_username";
    options.UserOptions.RoleClaim = "roles";
    options.UserOptions.ScopeClaim = "scope";
});

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BaseAddress"] ?? "http://localhost:8080");
})
.ConfigurePrimaryHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped<ICustomRequestClient, CustomRequestClient>();
builder.Services.AddScoped<IImageClient, ImageClient>();

await builder.Build().RunAsync();
