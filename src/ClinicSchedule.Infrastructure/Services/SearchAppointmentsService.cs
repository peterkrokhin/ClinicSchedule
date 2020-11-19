using ClinicSchedule.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ClinicSchedule.Infrastructure
{
    public class SearchAppointmentsService : ISearchAppointmentsService
    {
        public AppDbContext _appDbContext; 

        public SearchAppointmentsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Appointment>> GetNotLinkedAppointmentsByPatientNameAsync(string patientName)
        {
            var appointments = await _appDbContext.Appointment
                .Where(a => a.Patient.Name == patientName & a.Events.Count == 0)
                .ToListAsync();
            
            return appointments;
        }

        public async Task<List<Appointment>> GetNotLinkedAppointmentsByPatientIdAsync(int patientId)
        {
            
            var appointments = await _appDbContext.Appointment 
                .Where(a => a.PatientId == patientId & a.Events.Count == 0)
                .ToListAsync();
            
            return appointments;
        }
    }
}