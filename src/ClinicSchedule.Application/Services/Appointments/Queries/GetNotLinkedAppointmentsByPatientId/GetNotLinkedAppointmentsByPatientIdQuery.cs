using MediatR;

namespace ClinicSchedule.Application
{
    class GetNotLinkedAppointmentsByPatientIdQuery : IRequest<GetNotLinkedAppointmentsByPatientIdResponse>
    {
        public int Id { get; set; }

        public GetNotLinkedAppointmentsByPatientIdQuery(int id)
        {
            Id = id;
        }
    }
}