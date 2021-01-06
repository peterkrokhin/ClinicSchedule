using MediatR;

namespace ClinicSchedule.Application
{
    public class GetNotLinkedAppointmentsByPatientNameQuery : IRequest<GetNotLinkedAppointmentsByPatientNameResponse>
    {
        public string Name { get; set; }

        public GetNotLinkedAppointmentsByPatientNameQuery(string name)
        {
            Name = name;
        }
    }
}