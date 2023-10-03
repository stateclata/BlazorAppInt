using BlazorApp.Client;
using BlazorApp.Client.Services.CustomerService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


/*my Services*/
builder.Services.AddMudServices();
builder.Services.AddScoped<ICustomerService, CustomerService>();

/*************/
await builder.Build().RunAsync();
