using AutoMapper;
using ClinicSchedule.Core;

namespace ClinicSchedule.Application.Services.Events.Queries.GetAvailableDateEventsForAllPatientAppointments
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventResponse>();
        }
    }
}