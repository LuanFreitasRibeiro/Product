using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Data;
using ProductCatalog.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace ProductCatalog
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //Middlewares
            services.AddMvc();
            services.AddResponseCompression();

            services.AddScoped<StoreDataContext, StoreDataContext>();
            services.AddTransient<ProductRepository, ProductRepository>();
            services.AddTransient<CategoryRepository, CategoryRepository>();
            services.AddTransient<BrandRepository, BrandRepository>();

            services.AddSwaggerGen(x => {
                x.SwaggerDoc("v1", new Info { Title = "Store Catalog", Version="v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            //Middlewares
            app.UseMvc();
            app.UseResponseCompression();
            
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store Catalog");
            });
        }
    }
}
