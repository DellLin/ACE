using BlazorApp.Client.Pages;
using Google.Api.Gax;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Share;
using Share.Provider;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<ACE>("ace");
builder.Services.AddSingleton(_ => new FirestoreProvider(
    new FirestoreDbBuilder
    {
        ProjectId = "ace-cert-training",
        EmulatorDetection = EmulatorDetection.EmulatorOrProduction
    }.Build()
));
builder.Services.AddScoped<CacheStorageAccessor>();

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddRadzenComponents();
await builder.Build().RunAsync();
