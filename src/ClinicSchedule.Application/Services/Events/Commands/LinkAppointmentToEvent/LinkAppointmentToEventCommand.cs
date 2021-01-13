using System;
using System.Linq.Expressions;
using MediatR;
using ClinicSchedule.Core;
using FluentValidation.Results;

namespace ClinicSchedule.Application
{
    public class LinkAppointmentToEventCommand : IRequest
    {
        public int? AppointmentId { get; set; }
        public int EventId { get; set; }

        public LinkAppointmentToEventCommand(int? appointmemtId, int eventId)
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

        public ValidationResult Validate()
        {
            var validator = new LinkAppointmentToEventCommandValidator();
            var results = validator.Validate(this);
            return results;
        }
    }
}