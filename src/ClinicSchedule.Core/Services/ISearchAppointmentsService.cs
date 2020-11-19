using System.Threading.Tasks;
using System.Collections.Generic;

namespace ClinicSchedule.Core
{
    public interface ISearchAppointmentsService
    {
        Task<List<Appointment>> GetNotLinkedAppointmentsByPatientNameAsync(string patientName);
        Task<List<Appointment>> GetNotLinkedAppointmentsByPatientIdAsync(int patientId);
    }
}