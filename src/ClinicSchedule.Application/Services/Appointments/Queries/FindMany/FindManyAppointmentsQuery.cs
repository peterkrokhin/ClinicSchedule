using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ClinicSchedule.Core;
using MediatR;

namespace ClinicSchedule.Application
{
    public class FindManyAppointmentsQuery : IRequest<IEnumerable<FindManyAppointmentsResponse>>
    {
        private int _patientId { get; set; }
        private bool _isLinked { get; set; }

        public FindManyAppointmentsQuery(int patientId, bool isLinked)
        {
            _patientId = patientId;
            _isLinked = isLinked;
        }

        public Expression<Func<Appointment, bool>> GetPredicate()
        {  
            return (appointment) => appointment.PatientId == _patientId && 
                (_isLinked == true ? appointment.Events.Count != 0 : appointment.Events.Count == 0);
        }
    }
}