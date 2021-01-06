using System.Collections.Generic;
using MediatR;

namespace ClinicSchedule.Application.Services.Appointments.Queries.GetNotLinkedAppointmentsByPatientName
{
    public class Query : IRequest<IEnumerable<Response>>
    {
        public string Name { get; set; }

        public Query(string name)
        {
            Name = name;
        }
    }
}