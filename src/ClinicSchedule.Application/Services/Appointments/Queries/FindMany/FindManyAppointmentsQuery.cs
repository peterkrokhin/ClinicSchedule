using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediatR;
using ClinicSchedule.Core;

namespace ClinicSchedule.Application
{
    public class FindManyAppointmentsQuery : IRequest<IEnumerable<FindManyAppointmentsResponse>>
    {
        public int? PatientId { get; set; }
        public string PatientName { get; set; }
        public bool? IsLinked { get; set; }

        public Expression<Func<Appointment, bool>> GetPredicate()
        {
            Expression<Func<Appointment, bool>> predicate = (a) =>
                (PatientId != null ? a.PatientId == PatientId.Value : true) &
                (PatientName != null ? a.Patient.Name == PatientName : true) &
                (IsLinked != null ? (IsLinked.Value == true ? a.Events.Count != 0 : a.Events.Count == 0) : true);

            return predicate;
        }
    }
}