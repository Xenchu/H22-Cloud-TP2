using RapidAuto.MVC.Interfaces;
using RapidAuto.MVC.Services;


var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Host.ConfigureLogging(logging =>
{
    logging.AddConsole();
});

builder.Services.AddHttpClient<IFichierService, FichierServiceProxy>(client =>
    client.BaseAddress = new Uri(configuration.GetValue<string>("UrlFichierAPI")));

builder.Services.AddHttpClient<IVehiculeService, VehiculeServiceProxy>(client =>
    client.BaseAddress = new Uri(configuration.GetValue<string>("UrlVehiculeAPI")));

builder.Services.AddHttpClient<IUtilisateurService, UtilisateurServiceProxy>(client =>
    client.BaseAddress = new Uri(configuration.GetValue<string>("UrlUtilisateurAPI")));

builder.Services.AddHttpClient<ICommandeService, CommandeServiceProxy>(client =>
    client.BaseAddress = new Uri(configuration.GetValue<string>("UrlCommandeAPI")));

builder.Services.AddHttpClient<IFavorisService, FavorisServiceProxy>(client =>
    client.BaseAddress = new Uri(configuration.GetValue<string>("UrlFavorisAPI")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStatusCodePagesWithRedirects("/Home/CodeStatus?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
