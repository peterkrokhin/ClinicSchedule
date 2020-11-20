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
using ClinicSchedule.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace ClinicSchedule.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            string connectionString = Configuration.GetConnectionString("DefaultConnection"); 
            services.AddAppDbContext(connectionString);

            services.AddScoped<ISearchAppointmentsService, SearchAppointmentsService>();
            services.AddScoped<ISearchEventsService, SearchEventsService>();
            services.AddScoped<ILinkAppointmentsService, LinkAppointmentsService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
