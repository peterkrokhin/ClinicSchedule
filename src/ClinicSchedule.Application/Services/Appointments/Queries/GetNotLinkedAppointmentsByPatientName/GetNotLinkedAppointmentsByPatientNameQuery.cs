using MediatR;

namespace ClinicSchedule.Application
{
    class GetNotLinkedAppointmentsByPatientNameQuery : IRequest<GetNotLinkedAppointmentsByPatientNameResponse>
    {
        public string Name { get; set; }

        public GetNotLinkedAppointmentsByPatientNameQuery(string name)
        {
            Name = name;
        }
    }
}