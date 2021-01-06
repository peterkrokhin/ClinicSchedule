using System.Collections.Generic;
using MediatR;

namespace ClinicSchedule.Application
{
    public class GetNotLinkedAppointmentsByPatientNameQuery : 
        IRequest<IEnumerable<GetNotLinkedAppointmentsByPatientNameResponse>>
    {
        public string Name { get; set; }

        public GetNotLinkedAppointmentsByPatientNameQuery(string name)
        {
            Name = name;
        }
    }
}