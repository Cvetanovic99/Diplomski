using Diplomski.Application.Interfaces.ThirdPartyContracts;
using Diplomski.Infrastructure.Storage.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Infrastructure.Storage.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFileStorage(this IServiceCollection services)
        {
            services.AddTransient<IStorageService, StorageService>();

            return services;
        }

        public static IApplicationBuilder UseFileStorage(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            var path = Path.Combine(env.ContentRootPath, "uploads").ToLower();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "uploads")),
                RequestPath = "/uploads"
            });

            return app;
        }
    }
}
