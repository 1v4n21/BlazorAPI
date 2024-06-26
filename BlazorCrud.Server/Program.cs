using BlazorCrud.Server.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//A�adimos el servicio para poder acceder a la Base de datos mediante la cadenaSQL
builder.Services.AddDbContext<DbcrudBlazorContext>(opciones =>
{
	opciones.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
}

);

//Buildeamos politica Cors a nuestra API
builder.Services.AddCors(opciones =>
{
	opciones.AddPolicy("nuevaPolitica", app =>
	{
		app.AllowAnyOrigin()
		.AllowAnyHeader()
		.AllowAnyMethod();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//Aplicamos esta politica a nuestra App
app.UseCors("nuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
