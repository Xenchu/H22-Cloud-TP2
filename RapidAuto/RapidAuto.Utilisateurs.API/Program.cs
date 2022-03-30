using Microsoft.EntityFrameworkCore;
using RapidAuto.Utilisateurs.API.Data;
using RapidAuto.Utilisateurs.API.Interfaces;
using RapidAuto.Utilisateurs.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<UtilisateursContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));

builder.Services.AddScoped(typeof(IAsyncRepository), typeof(AsyncRepository));
builder.Services.AddScoped<IUtilisateursService, UtilisateursService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
