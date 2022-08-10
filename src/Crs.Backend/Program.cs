using Crs.Backend.Logic.Repositories.Implementations;
using Crs.Backend.Logic.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Crs.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = builder.Configuration;

            builder.Services.AddControllers(o => o.Conventions.Add(Conventions.DashedTokenTransformer.CreateConvention()));
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