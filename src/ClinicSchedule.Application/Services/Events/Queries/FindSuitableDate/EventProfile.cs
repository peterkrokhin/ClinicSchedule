using AutoMapper;
using ClinicSchedule.Core;

namespace ClinicSchedule.Application
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventResponse>();
        }
    }
}