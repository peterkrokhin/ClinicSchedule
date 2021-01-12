using System;
using System.Linq.Expressions;
using MediatR;
using ClinicSchedule.Core;

namespace ClinicSchedule.Application
{
    public class LinkAppointmentToEventCommand : IRequest
    {
        public int AppointmentId { get; set; }
        public int EventId { get; set; }

        public LinkAppointmentToEventCommand(int appointmemtId, int eventId)
        {
            AppointmentId = appointmemtId;
            EventId = eventId;
        }

        public Expression<Func<Event, bool>> GetEventPredicate()
        {
            Expression<Func<Event, bool>> predicate = (e) => 
                e.AppointmentId == AppointmentId;

            return predicate;
        }
    }
}