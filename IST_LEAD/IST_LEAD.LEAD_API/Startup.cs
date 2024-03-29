using dotenv.net.Utilities;
using IST_LEAD.BusinessLogic.Sevices;
using IST_LEAD.BusinessLogic.Sevices.ProductServices;
using IST_LEAD.Core;
using IST_LEAD.Core.Abstract;
using IST_LEAD.Core.Abstract.Services;
using IST_LEAD.Core.Models.Common;
using IST_LEAD.Core.ProductBuilder.Abstract;
using IST_LEAD.DAL;
using IST_LEAD.Integrations.Cloudinary;
using IST_LEAD.Integrations.Cloudinary.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Npgsql;
using IST_LEAD.DAL.Repository;
using IST_LEAD.Integrations.Directus;
using IST_LEAD.Integrations.Directus.Customs.Products;
using IST_LEAD.Integrations.Directus.Externals.Abstract;
using IST_LEAD.Integrations.Directus.Externals.Implementation;
using IST_LEAD.Integrations.Directus.Models;

namespace IST_LEAD.LEAD_API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFileManager, FileManager>();
            
            
            services.AddScoped<ICloudinaryManager>(x => new CloudinaryManager(new CloudinarySettings(
                                                        EnvReader.GetStringValue("ApiKey"),
                                                        EnvReader.GetStringValue("ApiSecret"),
                                                        EnvReader.GetStringValue("CloudName") )));

                
            
            var connectionString = EnvReader.GetStringValue("PostgreConnectionString");
            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            services.AddDbContext<DataContext>(options =>
            {
                options.
                    UseNpgsql(builder.ConnectionString,
                        assembly => assembly.MigrationsAssembly("IST_LEAD.DAL.PostgreSQL"));

            });
            
            services.AddScoped<IDirectusManager>(x => new DirectusManager(new DirectusProvider(
                                                    EnvReader.GetStringValue("BearerForAPI"),
                                                    EnvReader.GetStringValue("LocalDirectus")
                                                    )));
            
            services.AddScoped<IDbRepository, DbRepository>();
            services.AddTransient<IHandleExcelService, HandleExcelService>();
            
            services.AddTransient(typeof(IProductBuilderService<>), 
                typeof(ProductBuilderService<>));
            
            services.AddTransient<IVendCoderService, VendCoderService>();
            
            
            services.AddDistributedMemoryCache();
            services.AddSession();
    
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IST_LEAD.LEAD_API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseSession();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IST_LEAD.LEAD_API v1"));
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
            );
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
