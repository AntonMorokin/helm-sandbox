using Crs.Backend.Host.Conventions;
using Crs.Backend.Logic.Repositories.Implementations;
using Crs.Backend.Logic.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Crs.Backend
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = builder.Configuration;
            config.AddEnvironmentVariables("CRS_BACKEND_");

            builder.Services
                .AddControllers(o => o.Conventions.Add(DashedTokenTransformer.CreateConvention()))
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<Data.DataContext>(o =>
            {
                var connectionString = config["DbConnectionString"];

                o.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
            });

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
            builder.Services.AddScoped<ICarsRepository, CarsRepository>();
            builder.Services.AddScoped<IRidesRepository, RidesRepository>();

            var app = builder.Build();

            await MigrateDatabaseAsync(app.Logger, app.Services);

            var pathBase = config["PathBase"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
                app.UseRouting();

                app.Logger.LogInformation($"PathBase has been set to {pathBase}");
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers();

            app.MapFallback((HttpContext context) =>
            {
                return $"Can not find appropriate handler. Path base: {context.Request.PathBase}; path: {context.Request.Path}";
            });

            app.Run();
        }

        private static async Task MigrateDatabaseAsync(ILogger logger, IServiceProvider services)
        {
            var scopeFactory = services.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();

            using var dbContext = scope.ServiceProvider.GetRequiredService<Data.DataContext>();

            logger.LogInformation("Starting migrating the database");

            try
            {
                await dbContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while migrating the database");
                throw;
            }

            logger.LogInformation("The database has been successfully migrated");
        }
    }
}