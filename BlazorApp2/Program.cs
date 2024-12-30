using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorApp2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddOidcAuthentication(options =>
            {

                builder.Configuration.Bind("Auth0", options.ProviderOptions);
                options.ProviderOptions.ResponseType = "code";

            //options.Events = new OpenIdConnectEvents
            //{
            //    // handle the logout redirection
            //    OnRedirectToIdentityProviderForSignOut = (context) =>
            //    {
            //        var logoutUri = $"https://{Configuration["Auth0:Domain"]}/v2/logout?client_id={Configuration["Auth0:ClientId"]}";

            //        var postLogoutUri = context.Properties.RedirectUri;
            //        if (!string.IsNullOrEmpty(postLogoutUri))
            //        {
            //            if (postLogoutUri.StartsWith("/"))
            //            {
            //                // transform to absolute
            //                var request = context.Request;
            //                postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
            //            }
            //            logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
            //        }

            //        context.Response.Redirect(logoutUri);
            //        context.HandleResponse();

            //        return Task.CompletedTask;
            //    }
            });

            await builder.Build().RunAsync();
        }
    }
}
