using MediatR;

namespace ClinicSchedule.Application
{
    public class GetNotLinkedAppointmentsByPatientIdQuery : IRequest<GetNotLinkedAppointmentsByPatientIdResponse>
    {
        public int Id { get; set; }

        public GetNotLinkedAppointmentsByPatientIdQuery(int id)
        {
            Id = id;
        }
    }
}