using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductCatalog.Application;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Application.Service;
using ProductCatalog.Application.Service.Abstraction;
using ProductCatalog.Application.Settings;
using ProductCatalog.Application.Validators;
using ProductCatalog.Data;
using ProductCatalog.Domain;
using ProductCatalog.Domain.Response;
using ProductCatalog.Domain.Response.Brand;
using ProductCatalog.Domain.Response.Category;
using ProductCatalog.Domain.Response.Product;
using ProductCatalog.Domain.Response.User;
using ProductCatalog.Repository;
using ProductCatalog.Repository.Abstraction;
using ProductCatalog.Repository.Interfaces;
using System.Linq;
using System.Text;

namespace ProductCatalog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Middlewares
            services.AddCors();
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            services.AddControllers();
            services.AddControllersWithViews();

            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => 
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddDbContext<StoreDataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("connectionString")));
            //services.AddScoped<StoreDataContext, StoreDataContext>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Brand, CreateBrandResponse>();
                cfg.CreateMap<Brand, GetBrandResponse>();
                cfg.CreateMap<Brand, UpdateBrandResponse>();

                cfg.CreateMap<Category, CreateCategoryResponse>();
                cfg.CreateMap<Category, GetCategoryResponse>();
                cfg.CreateMap<Category, UpdateCategoryResponse>();

                cfg.CreateMap<Product, CreateProductResponse>();
                cfg.CreateMap<Product, GetProductResponse>();
                cfg.CreateMap<Product, UpdateProductResponse>();

                cfg.CreateMap<User, CreateUserResponse>();
                cfg.CreateMap<User, GetUserResponse>();
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);

            services.AddScoped<BrandValidator, BrandValidator>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSwaggerGen(x => {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Store Catalog", Version="v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            //Middlewares
            app.UseResponseCompression();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store Catalog");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
