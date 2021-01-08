using MediatR;

namespace ClinicSchedule.Application.Services.Events.Commands.LinkAppointmentToEvent
{
    public class Command : IRequest<int>
    {
        public int AppointmentId { get; set; }
        public int EventId { get; set; }
    }
}