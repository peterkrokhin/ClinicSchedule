using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace ClinicSchedule.Application
{
    public interface IQuerryAggregator : IDisposable
    {
        Task<DateEvents> GetAvailableDateEventsForAllPatientAppointmentsAsync(int patientId);
        Task TryLinkAppointmentToEventAsync(int appointmentId, int eventId);
    }
}