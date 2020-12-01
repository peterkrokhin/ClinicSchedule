using System;
using ClinicSchedule.Application;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


namespace ClinicSchedule.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAppDbContext DbContext { get; set; }
        public IAppointmentRepository Appointments { get; set; }
        public IEventRepository Events { get; set; }

        public UnitOfWork(IAppDbContext appDbContext)
        {
            DbContext = appDbContext;
            Appointments = new AppointmentRepository(appDbContext);
            Events = new EventRepository(appDbContext);
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if(disposed)
                return;

            if(disposing)
            {
                DbContext?.Dispose();
                Appointments?.Dispose();
                Events?.Dispose();
                // Console.WriteLine($"object {this.ToString()} Dispose"); // Проверка работы Dispose()
            }

            disposed = true;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

