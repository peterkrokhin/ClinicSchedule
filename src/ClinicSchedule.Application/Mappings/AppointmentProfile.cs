using AutoMapper;
using ClinicSchedule.Core;

namespace ClinicSchedule.Application
{
    class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, GetNotLinkedAppointmentsByPatientIdResponse>();
        }
    }
}