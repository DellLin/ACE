using BlazorApp.Client.Pages;
using BlazorApp.Components;
using BlazorApp.Settings;
using Google.Api.Gax;
using Google.Cloud.Firestore;
using Radzen;
using Share.Provider;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var firebaseJson = JsonSerializer.Serialize(new FirebaseSettings());

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSingleton(_ => new FirestoreProvider(
    new FirestoreDbBuilder
    {
        ProjectId = "ace-cert-training",
        EmulatorDetection = EmulatorDetection.EmulatorOrProduction
    }.Build()
));
builder.Services.AddRadzenComponents();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

await app.RunAsync();
