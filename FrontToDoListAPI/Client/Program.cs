global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;
using FrontToDoListAPI.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FrontToDoListAPI.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IAssignmentsService, AssignmentsService>();
//builder.Services.AddAuthorizationCore();
//builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();