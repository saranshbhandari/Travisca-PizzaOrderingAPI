using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PizzaOrderingSystem.Ordering.API.Models;
using PizzaOrderingSystem.Ordering.API.Services;
using PizzaOrderingSystem.Ordering.API.Authentication;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.Net.Http.Headers;

namespace PizzaOrderingSystem.Ordering.API
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



            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetValue<string>("Authentication:hashSecret").ToString())),
                        ValidateIssuer = false,
                        ValidateAudience = false

                    };
                }


            );
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PizzaOrderingSystem.Ordering.API", Version = "v1" });

                c.AddSecurityDefinition("token", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. " + Environment.NewLine +
                      @"Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = HeaderNames.Authorization,
                    Scheme = "Bearer"
                });
                c.OperationFilter<SecureEndpointAuthRequirementFilter>();


            });

            //Loading settings from appsettings
            services.Configure<PizzaOrderingSystemDBSettings>(Configuration.GetSection("PizzaOrderingSystemDatabase"));
            services.Configure<AuthenticationSettings>(Configuration.GetSection("Authentication"));

            //Injecting Db services
            services.AddSingleton<IMenuItemService, MenuItemService>();
            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IItemCategoryService, ItemCategoryService>();
            services.AddSingleton<IUserService, UserService>();

            services.AddSingleton<IJwtUtility, JWTUtility>();





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaOrderingSystem.Ordering.API v1"));
            }

            app.UseHttpsRedirection();

            
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(x =>
          x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()

          );



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
