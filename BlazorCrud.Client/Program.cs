using BlazorCrud.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Ruta donde se encuentra nuestra api
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5064") });

await builder.Build().RunAsync();
