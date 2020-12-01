using ClinicSchedule.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using ClinicSchedule.Application;

namespace ClinicSchedule.Infrastructure
{
    public class AppointmentRepository: GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(IAppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<IEnumerable<Appointment>> GetNotLinkedAppointmentsByPatientNameAsync(string patientName)
        {
            return await DbSet
                .Where(a => a.Patient.Name == patientName & a.Events.Count == 0)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetNotLinkedAppointmentsByPatientIdAsync(int patientId)
        {
            return await DbSet 
                .Where(a => a.PatientId == patientId & a.Events.Count == 0)
                .ToListAsync();
        }

        public IEnumerable<AppointmentDTO> ConvertAllToDTO(IEnumerable<Appointment> appointments)
        {
            return appointments
                .Select(a => new AppointmentDTO
                    {
                        Id = a.Id,
                        PatientId = a.PatientId,
                        ServiceId = a.ServiceId,
                    });
            
        }

        public async Task<Appointment> GetByIdIncludeEventsAsync(int id)
        {
            return await DbSet
                .Include(a => a.Events)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        
    }
}