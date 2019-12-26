using System.Text.Json.Serialization;
using FileApplication.BL.Providers;
using FileApplication.BL.Repositories;
using FileApplication.BL.Services;
using FileApplication.Data.Providers;
using FileApplication.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddScoped<ITreeService, TreeService>();
            services.AddScoped<IItemFactory, ItemFactory>();
            services.AddScoped<ITreeBuilder, TreeBuilder>();
            
            // Repositories.
            services.AddScoped<IFileRepository, FileInMemoryRepository>();
            services.AddScoped<IFolderRepository, FolderInMemoryRepository>();
            services.AddScoped<IFileStoreProvider, FileStoreInMemoryProvider>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FileApp API", Version = "v1" });
            });
            
            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new ItemTypeJsonConverterFactory());
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
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
 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}