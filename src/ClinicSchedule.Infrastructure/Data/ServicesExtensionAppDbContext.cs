using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicSchedule.Infrastructure
{
    public static class ServicesExtensionAppDbContext
    {
        public static void AddAppDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
        }
    }
}
