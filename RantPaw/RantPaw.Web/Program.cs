using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RantPaw.Services.Web.PostServices;
using RantPaw.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetSection("BaseAddress").Value) });

// Web services

builder.Services.AddScoped<IPostService, PostService>();

await builder.Build().RunAsync();
