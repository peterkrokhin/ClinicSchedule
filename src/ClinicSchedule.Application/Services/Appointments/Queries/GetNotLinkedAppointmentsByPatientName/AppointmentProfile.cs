using AutoMapper;
using ClinicSchedule.Core;

namespace ClinicSchedule.Application.Services.Appointments.Queries.GetNotLinkedAppointmentsByPatientName
{
    class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, Response>();
        }
    }
}