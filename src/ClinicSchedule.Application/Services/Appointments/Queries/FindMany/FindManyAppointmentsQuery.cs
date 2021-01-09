using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediatR;
using ClinicSchedule.Core;

namespace ClinicSchedule.Application
{
    public class FindManyAppointmentsQuery : IRequest<IEnumerable<FindManyAppointmentsResponse>>
    {
        private int? _patientId { get; set; }
        private string _patientName { get; set; }
        private bool? _isLinked { get; set; }

        public FindManyAppointmentsQuery(int? patientId, string patientName, bool? isLinked)
        {
            _patientId = patientId;
            _patientName = patientName;
            _isLinked = isLinked;
        }

        public Expression<Func<Appointment, bool>> GetPredicate()
        {
            Expression<Func<Appointment, bool>> predicate = (a) =>
                (_patientId != null ? a.PatientId == _patientId.Value : true) &
                (_patientName != null ? a.Patient.Name == _patientName : true) &
                (_isLinked != null ? (_isLinked.Value == true ? a.Events.Count != 0 : a.Events.Count == 0) : true);

            return predicate;
        }
    }
}