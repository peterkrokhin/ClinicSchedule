using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ClinicSchedule.Application;
using ClinicSchedule.Infrastructure;

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
            app.UseSerilogRequestLogging();
            app.UseMiddleware<RequestResponseLogMiddleware>();

            app.UseExceptionHandler("/api/errors");

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
