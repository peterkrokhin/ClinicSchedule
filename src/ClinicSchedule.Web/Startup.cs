using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ClinicSchedule.Core;
using ClinicSchedule.Application;
using ClinicSchedule.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace ClinicSchedule.Web
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Поддержка Json Patch
            services.AddControllers().
                AddNewtonsoftJson();

            services.AddAppServices();

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddAppInfrastructure(connectionString);

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/api/errors");

            app.UseMiddleware<RequestResponseLogMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClinicSchedule");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
