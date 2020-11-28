using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace ClinicSchedule.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IAppointmentRepository Appointments { get; set; }
        IEventRepository Events { get; set; }
        Task<DateEvents> GetAvailableDateEventsForAllPatientAppointmentsAsync(int patientId);
        Task TryLinkAppointmentToEventAsync(int appointmentId, int eventId);
        Task SaveChangesAsync();
    }
}