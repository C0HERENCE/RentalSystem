using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RentalSystem.Dao;
using RentalSystem.Models;
using RentalSystem.Services;
using VueCliMiddleware;

namespace RentalSystem
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
            // 注入Controllers和Vue项目, 设置controller的json解析库为NewtonsoftJson(支持dynamic传输对象)
            services.AddControllers().AddNewtonsoftJson();
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "client-app/dist"; });

            // 注入swagger
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    In = ParameterLocation.Header, 
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey 
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    { 
                        new OpenApiSecurityScheme 
                        { 
                            Reference = new OpenApiReference 
                            { 
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer" 
                            } 
                        },
                        new string[] { } 
                    } 
                });
            });
            
            // 注入Jwt
            var jwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.AddSingleton(jwtConfig);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
                        ValidIssuer = jwtConfig.Issuer,
                        ValidAudience = jwtConfig.Audience,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                    };
                });

            services.AddScoped<JwtService>();

            services.AddDbContext<RentalSystemDbContext>(builder => builder.UseSqlServer(Configuration["RentalSystem:ConnectionString"]));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSpaStaticFiles();

            // swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(builder =>
            {
                builder.MapControllerRoute("default", "{controller}/{action}/{id?}");
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client-app";
            
                if (env.IsDevelopment())
                {
                    spa.UseVueCli();
                }
            });
        }
    }
}