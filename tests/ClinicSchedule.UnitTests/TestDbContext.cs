using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClinicSchedule.Core;
using ClinicSchedule.Infrastructure;

namespace ClinicSchedule.UnitTests
{
    class TestDbContext : DbContext, IAppDbContext
    {
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Event> Event { get; set; }
        
        // Методы Set<T>(), SaveChangesAsync() берем из базового класса
        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Data Source=../../../../testForTests.sqlite");
        }

    }

}
