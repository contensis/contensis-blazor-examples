using BlazorWebAssemblyApp.Client;
using BlazorWebAssemblyApp.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var uriString = builder.HostEnvironment.BaseAddress;
builder.Services.AddHttpClient<IManagementService, ManagementService>(client => client.BaseAddress = new Uri(uriString));
builder.Services.AddHttpClient<IDeliveryService, DeliveryService>(client => client.BaseAddress = new Uri(uriString));

await builder.Build().RunAsync();
