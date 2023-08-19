using HttpSender;
using Marvel;
using Marvel.Database;
using Marvel.Database.Context;
using Marvel.Models.Settings;
using Marvel.Services;
using Microsoft.EntityFrameworkCore;
using Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;

services
    .AddSwagger()
    .AddServices()
    .AddDatabase()
    .AddConverters()
    .AddControllers();

services.Configure<Settings>(builder.Configuration.GetSection("Settings"));

var connectionString = builder.Configuration.GetConnectionString("Marvel");

services.AddDbContext<MarvelContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler(o => o.Run(async context =>
{
    await context.RequestServices.GetRequiredService<IExceptionHandler>().InvokeAsync(context);
}));

if (String.IsNullOrEmpty(connectionString) || connectionString == "ConnectionString")
{
    throw new Exception("ConnectionString should be set in appsettings.json.");
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
