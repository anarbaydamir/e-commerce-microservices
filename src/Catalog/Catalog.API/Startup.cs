using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Dal.Impl;
using Catalog.API.Dal.Inter;
using Catalog.API.Repository.Impl;
using Catalog.API.Repository.Inter;
using Catalog.API.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Catalog.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<CatalogDatabaseSettings>(Configuration.GetSection(nameof(CatalogDatabaseSettings)));
            services.AddSingleton<ICatalogDatabaseSettings>(sp => sp.GetRequiredService<IOptions<CatalogDatabaseSettings>>().Value);

            services.AddTransient<ICatalogContext, CatalogContext>();
            services.AddTransient<IProductRepository, ProductRepository>();

            //swagger
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = doc =>
                {
                    doc.Info.Version = "v1";
                    doc.Info.Title = "Catalog API";
                    doc.Info.Description = "Catalog API for Microservices";
                    doc.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Anar Baydamirov",
                        Email = "anarbaydamir@gmail.com",
                        Url = "https://github.com/anarbaydamirov"
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //swagger
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
