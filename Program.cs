using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AspNetApi.Data;
using AspNetApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={DatabaseHelper.GetDatabasePath()}")
);

builder.Services.AddControllers();

//auto registrar servicios
builder.Services.Scan(scan => scan
    .FromAssemblies(Assembly.GetExecutingAssembly())
    .AddClasses(classes => classes.Where(type => type.Namespace?.EndsWith("Services") == true))
    .AsSelf()
    .WithTransientLifetime());

var app = builder.Build();

// Aplicar las migraciones pendientes al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Comprobar si hay migraciones pendientes y aplicarlas
    dbContext.Database.Migrate();
}

var lifetime = app.Lifetime;
lifetime.ApplicationStopping.Register(() =>
{
    var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.CloseConnection(); // Cerrar la conexión explícitamente si es necesario
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();