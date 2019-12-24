using FileApplication.BL.Providers;
using FileApplication.BL.Repositories;
using FileApplication.BL.Services;
using FileApplication.Data.Providers;
using FileApplication.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FileApplication
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Services.
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFolderService, FolderService>();
            services.AddScoped<IItemFactory, ItemFactory>();
            
            // Repositories.
            services.AddScoped<IFileRepository, FileInMemoryRepository>();
            services.AddScoped<IFolderRepository, FolderInMemoryRepository>();
            services.AddScoped<IFileStoreProvider, FileStoreProvider>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FileApp API", Version = "v1" });
            });
            
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FileApp API V1");
            });
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
        }
    }
}