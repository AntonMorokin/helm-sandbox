using Crs.Backend.Host.Conventions;
using Crs.Backend.Logic.Repositories.Implementations;
using Crs.Backend.Logic.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crs.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = builder.Configuration;

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

            var pathBase = config["PathBase"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.Run();
        }
    }
}