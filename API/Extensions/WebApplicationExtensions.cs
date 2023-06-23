using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class WebApplicationExtensions
    {
      public static async Task DbUpdateAndRunAsync(this WebApplication app, string? url = null)
{
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
var context = services.GetRequiredService<DataContext>();
await context.Database.MigrateAsync();
await Seed.SeedUsers(context);
}
catch(Exception ex)
{
var logger = services.GetRequiredService<ILogger<Program>>();
logger.LogError(ex, "A aparut o eroare pe timpul migrarii.");
}
await app.RunAsync();
}

    }
}