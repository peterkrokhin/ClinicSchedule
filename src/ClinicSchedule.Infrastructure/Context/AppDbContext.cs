using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClinicSchedule.Core;
using ClinicSchedule.Application;

namespace ClinicSchedule.Infrastructure
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Event> Event { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Методы Set<T>(), SaveChangesAsync() берем из базового класса
        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
