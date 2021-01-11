using System;
using System.Linq.Expressions;
using ClinicSchedule.Core;
using MediatR;

namespace ClinicSchedule.Application
{
    public class FindSuitableDateQuery : IRequest<FindSuitableDateResponse>
    {
        private readonly int _patientId;

        public FindSuitableDateQuery(int patientId)
        {
            _patientId = patientId;
        }

        public Expression<Func<Appointment, bool>> GetAppointmentPredicate()
        {
            Expression<Func<Appointment, bool>> predicate = (a) => a.PatientId == _patientId & a.Events.Count == 0;

            return predicate;
        }

        public Expression<Func<Event, bool>> GetEventPredicate()
        {
            Expression<Func<Event, bool>> predicate = (e) =>
                e.AppointmentId == null;

            return predicate;
        }

    }
}