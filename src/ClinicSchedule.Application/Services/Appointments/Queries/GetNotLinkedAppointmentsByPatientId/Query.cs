using System.Collections.Generic;
using MediatR;

namespace ClinicSchedule.Application.Services.Appointments.Queries.GetNotLinkedAppointmentsByPatientId
{
    public class Query : IRequest<IEnumerable<Response>>
    {
        public int Id { get; set; }

        public Query(int id)
        {
            Id = id;
        }
    }
}