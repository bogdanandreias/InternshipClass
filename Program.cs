using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RazorMvc.Data;
using System;

namespace RazorMvc
{
    public class Program
    {
        public static void Main(string[] args) 
        {
        var host = CreateHostBuilder(args).Build();

        CreateDbIfNotExists(host);

        host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<InternDbContext>();
                var webHostEnvironment = services.GetRequiredService< IWebHostEnvironment>();
                if (webHostEnvironment.IsDevelopment())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                    }

                SeedData.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }


    public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
