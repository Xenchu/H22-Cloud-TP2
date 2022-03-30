using Microsoft.EntityFrameworkCore;
using RapidAuto.Commandes.API.Data;
using RapidAuto.Commandes.API.Interfaces;
using RapidAuto.Commandes.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CommandeContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));

builder.Services.AddScoped(typeof(IAsyncRepository), typeof(AsyncRepository));
builder.Services.AddScoped<ICommandesService, CommandesService>();

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
