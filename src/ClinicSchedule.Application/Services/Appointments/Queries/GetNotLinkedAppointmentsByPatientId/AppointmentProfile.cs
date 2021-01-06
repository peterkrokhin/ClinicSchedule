using AutoMapper;
using ClinicSchedule.Core;

namespace ClinicSchedule.Application.Services.Appointments.Queries.GetNotLinkedAppointmentsByPatientId
{
    class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, Response>();
        }
    }
}