using ClinicSchedule.Core;
using ClinicSchedule.Application;

namespace ClinicSchedule.Infrastructure
{
    public class AppointmentRepository: GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(IAppDbContext appDbContext) : base(appDbContext)
        {
        }  
    }
}