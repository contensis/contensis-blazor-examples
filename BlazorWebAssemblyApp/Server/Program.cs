using BlazorWebAssemblyApp.Server;
using BlazorWebAssemblyApp.Server.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Zengenti.Contensis.Delivery;
using Zengenti.Contensis.Management;
using Zengenti.Rest.RestClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var managementClient = ManagementClient.Create(
    rootUrl: Settings.RootUri,
    clientId: Settings.ClientId,
    sharedSecret: Settings.SharedSecret);
builder.Services.AddScoped(_ => managementClient);

var contensisClient = ContensisClient.Create(
    rootUrl: Settings.RootUri,
    projectId: Settings.DeliveryProjectApiId,
    versionStatus: Settings.DeliveryVersionStatus,
    clientId: Settings.ClientId,
    sharedSecret: Settings.SharedSecret);
builder.Services.AddScoped(_ => contensisClient);

var secureRestClient =
    new RestClientFactory(Settings.RootUri).SecuredRestClient
    (
        "authenticate",
        Settings.ClientId,
        Settings.SharedSecret,
        Settings.Scopes
    );
builder.Services.AddSingleton<IRestClient, RestClient>(x => secureRestClient);
builder.Services.AddScoped<IUiContentTypeService, UiContentTypeService>();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
