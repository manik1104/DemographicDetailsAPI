using DemographicDetails.Infrastructure.Implementation;
using DemographicDetails.Infrastructure.Interfaces;
using DemographicDetails.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemographicDetails
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
            services.AddScoped<IDistanceCalcualtionService, DistanceCalcualtionService>();
            services.AddScoped<IGeoLocationRepository, GeoLocationRepository>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DemographicDetails.Api",
                    Description = "Testing"
                });
            });
            services.AddLogging(c => c.AddConsole());

            services.AddMvc();

                //Default Policy
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.WithOrigins("https://localhost:44351", "http://localhost:3000")
                                                .AllowAnyHeader()
                                                .AllowAnyMethod();
                        });
                });

                // Named Policy
                services.AddCors(options =>
                {
                    options.AddPolicy(name: "AllowOrigin",
                        builder =>
                        {
                            builder.WithOrigins("https://localhost:44351", "http://localhost:4200")
                                                .AllowAnyHeader()
                                                .AllowAnyMethod();
                        });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemographicDetails.Api");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            // CORS: UseCors with CorsPolicyBuilder.
            app.UseCors();

            // with a named pocili
            app.UseCors("AllowOrigin");

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

 
        }

        //private void RegisterServices(IServiceCollection services)
        //{
        //    services.AddScoped<IDistanceCalcualtionService, DistanceCalcualtionService>();
        //}
    }
}
