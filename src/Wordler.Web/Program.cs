using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Wordler.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton<IMixerService, MixerService>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();
