using BlazorCrrud.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CurrieTechnologies.Razor.SweetAlert2;
using BlazorCrrud.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Ruta donde se encuentra nuestra api
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5064") });

//Añadimos al constructor el servicio de Departamentos y de Empleados
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();

//Añadimos al constructor el plugin de alertas SweetAlert
builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
