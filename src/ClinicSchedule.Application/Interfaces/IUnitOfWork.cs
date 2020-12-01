using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace ClinicSchedule.Application
{
    public interface IUnitOfWork : IDisposable
    {
        IAppointmentRepository Appointments { get; set; }
        IEventRepository Events { get; set; }
        Task SaveChangesAsync();
    }
}