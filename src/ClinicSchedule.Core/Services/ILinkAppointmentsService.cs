using System.Threading.Tasks;
using System.Collections.Generic;

namespace ClinicSchedule.Core
{
    public interface ILinkAppointmentsService
    {
        Task<string> LinkAppointmentToEvent(int appointmentId, int eventId);
    }
}