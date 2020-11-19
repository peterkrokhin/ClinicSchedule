using System;
using Microsoft.EntityFrameworkCore;
using ClinicSchedule.Core;

namespace ClinicSchedule.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Event> Event { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
