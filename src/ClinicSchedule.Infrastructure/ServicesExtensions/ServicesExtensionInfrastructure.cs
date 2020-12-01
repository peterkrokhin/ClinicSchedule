using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ClinicSchedule.Application;


namespace ClinicSchedule.Infrastructure
{
    public static class ServicesExtensionInfrastructure
    {
        public static void AddAppInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseSqlite(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
        }
    }
}
