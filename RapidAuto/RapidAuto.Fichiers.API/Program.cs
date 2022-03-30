using RapidAuto.Fichiers.API.Interfaces;
using RapidAuto.Fichiers.API.Services;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAzureClients(configure =>
{
    // Add a Storage account client
    configure.AddBlobServiceClient(builder.Configuration.GetConnectionString("StorageConnectionString"));
});

// Add services to the container.
builder.Services.AddScoped<IFichiersService, FichiersService>();
builder.Services.AddScoped<IStorageServiceHelper, StorageServiceHelper>();

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

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
