using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClinicSchedule.Core;

namespace ClinicSchedule.Application
{
    public interface IAppDbContext : IDisposable
    {
        DbSet<Patient> Patient { get; set; }
        DbSet<Service> Service { get; set; }
        DbSet<Appointment> Appointment { get; set; }
        DbSet<Event> Event { get; set; }
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync();
    }
}
