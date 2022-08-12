using APIRest.Contextos;
using APIRest.Seguridad;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIRest
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

            var cnnString = Configuration.GetConnectionString("MSSqlServer");
            services.AddDbContext<Context>(optionsBuilder => optionsBuilder.UseSqlServer(cnnString));
            services.AddScoped<Context>();

            //Review: Configurar Autenticacion Basica
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("BasicAuthentication", null);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "https://localhost:5001",
                    ValidAudience = "https://localhost:5001",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("contraseñasupersecreta"))
                    
                };
            });

            services.AddHealthChecks()
                .AddCheck("App Running", 
                ()=>
                {
                       return HealthCheckResult.Healthy("Api is working as expected");
                })
                .AddSqlServer(
                    cnnString,
                    healthQuery:"select 1",
                    name:"SQL Server Running",
                    failureStatus: HealthStatus.Unhealthy
                )
                .AddUrlGroup(
                    new Uri("https://google.com"),
                    name: "Internet Online",
                    failureStatus: HealthStatus.Unhealthy
                )
                .AddUrlGroup(
                    new Uri("https://bisoftguard.azurewebsites.net/.well-known/openid-configuration"),
                    name: "Security Server Online",
                    failureStatus: HealthStatus.Unhealthy
                )
                ;

            services.AddHealthChecksUI(setup=>
                {
                    setup.MaximumHistoryEntriesPerEndpoint(2);
                })
                .AddInMemoryStorage();
                
        
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIRest", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIRest v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthcheck", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecksUI(setupOptions: s =>
                {
                    s.AddCustomStylesheet("recursos/styles.css");
                }
                );
            });
        }
    }
}
