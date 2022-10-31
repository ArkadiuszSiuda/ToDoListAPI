using Blazored.LocalStorage;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ToDoListAPIFront;
using ToDoListAPIFront.Auth;
using ToDoListAPIFront.Helpers;
using ToDoListAPIFront.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMatToaster(config =>
{
    config.Position = MatToastPosition.BottomCenter;
    config.PreventDuplicates = true;
    config.NewestOnTop = true;
    config.ShowCloseButton = true;
    config.MaximumOpacity = 95;
    config.VisibleStateDuration = 7000;
});
builder.Services.AddScoped<SpinnerService>();
builder.Services.AddScoped<SpinnerHandler>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://todolistapi20221031132846.azurewebsites.net") });
builder.Services.AddScoped(sp =>
{
    SpinnerHandler spinHandler = sp.GetRequiredService<SpinnerHandler>();
    spinHandler.InnerHandler = new HttpClientHandler();
    NavigationManager navManager = sp.GetRequiredService<NavigationManager>();
    return new HttpClient(spinHandler)
    {
        BaseAddress = new Uri("https://todolistapi20221031132846.azurewebsites.net")
    };
});

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddMatBlazor();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();