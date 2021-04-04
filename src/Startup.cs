using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application.Service;
using ProductCatalog.Application.Service.Abstraction;
using ProductCatalog.Application.Validators;
using ProductCatalog.Data;
using ProductCatalog.Repository;
using ProductCatalog.Repository.Abstraction;
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
            services.AddScoped<BrandValidator, BrandValidator>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBrandService, BrandService>();

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
