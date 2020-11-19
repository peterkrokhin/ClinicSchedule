using System.Threading.Tasks;
using System.Collections.Generic;

namespace ClinicSchedule.Core
{
    public interface ISearchEventsService
    {
        Task<AvailableDateEvents> GetAvailableDateEventsForAllPatientAppointments(int patientId);
    }
}