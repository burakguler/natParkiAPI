using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkiAPI.Data;
using ParkiAPI.Repository.IRepository;
using AutoMapper;
using ParkiAPI.ParkiMapper;

namespace ParkiAPI
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
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("natParkiConnection")));

            services.AddScoped<INationalParkRepository, NationalParkRepository>();
            /* ^ after this code you can access the national park repository on any of controllers
                Creates an instance for each incoming web request and 
                uses the same instance for each incoming request, 
                creates a new instance for different web requests. ~Burak */
            services.AddAutoMapper(typeof(ParkiMapping));
            services.AddSwaggerGen(options=>
            {
                options.SwaggerDoc("natParkiOpenAPISpec",
                    new Microsoft.OpenApi.Models.OpenApiInfo() 
                    {
                        Title = "natParki API",
                        Version = "1"
                    });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/swagger/natParkiOpenAPISpec/swagger.json", "natParki API");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
