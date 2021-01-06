using System.Collections.Generic;
using MediatR;

namespace ClinicSchedule.Application
{
    public class GetNotLinkedAppointmentsByPatientIdQuery 
        : IRequest<IEnumerable<GetNotLinkedAppointmentsByPatientIdResponse>>
    {
        public int Id { get; set; }

        public GetNotLinkedAppointmentsByPatientIdQuery(int id)
        {
            Id = id;
        }
    }
}