global using BlazorApp.Client.Services.BingService;
global using BlazorApp.Client.Services.CustomerService;
global using BlazorApp.Client.Services.UserService;
global using BlazorApp.Shared;
global using System.Net.Http.Json;
global using Microsoft.AspNetCore.Components.Authorization;
using BlazorApp.Client;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


/*my Services*/
builder.Services.AddMudServices();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IBingService, BingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
/*************/
await builder.Build().RunAsync();
