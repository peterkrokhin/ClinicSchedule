using System.Threading.Tasks;
using System.Collections.Generic;

namespace ClinicSchedule.Core
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetNotLinkedAppointmentsByPatientNameAsync(string patientName);
        Task<IEnumerable<Appointment>> GetNotLinkedAppointmentsByPatientIdAsync(int patientId);
        IEnumerable<AppointmentDTO> ConvertAllToDTO(IEnumerable<Appointment> appointments);
        Task<Appointment> GetByIdIncludeEventsAsync(int id);
    }
}