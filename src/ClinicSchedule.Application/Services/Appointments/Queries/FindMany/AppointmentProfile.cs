using AutoMapper;
using ClinicSchedule.Core;

namespace ClinicSchedule.Application
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, FindManyAppointmentsResponse>();
        }
    }
}