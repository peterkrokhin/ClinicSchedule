using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ClinicSchedule.Application;

namespace ClinicSchedule.Infrastructure
{
    public static class ServicesExtensionAppDbContext
    {
        public static void AddAppDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseSqlite(connectionString));
        }
    }
}
