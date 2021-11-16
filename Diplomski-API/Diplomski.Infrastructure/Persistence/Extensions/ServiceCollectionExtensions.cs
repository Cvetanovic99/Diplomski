using Diplomski.Application.Interfaces.ThirdPartyContracts;
using Diplomski.Infrastructure.Persistence.Contexts;
using Diplomski.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Infrastructure.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("Diplomski.Infrastructure"));
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IFileTypeRepository, FileTypeRepository>();

            return services;
        }
    }
}
