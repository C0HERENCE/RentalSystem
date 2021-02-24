using System.Data.SqlClient;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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
            // 注入Controllers和Vue项目
            services.AddControllers();
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "client-app/dist"; });

            // 注入swagger
            services.AddSwaggerGen();
            
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

            // 注入sqlConnection，从appsettings中读取连接字符串，自动打开连接，自动Dispose
            services.AddTransient(provider =>
            {
                var sqlConnection = new SqlConnection(Configuration.GetConnectionString("RentalSystem"));
                sqlConnection.Open();
                return sqlConnection;
            });
            
            // 注入Service和Dao
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDao, UserDao>();
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