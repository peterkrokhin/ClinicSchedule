using MediatR;

namespace ClinicSchedule.Application
{
    public class LinkAppointmentToEventCommand : IRequest
    {
        public int AppointmentId { get; set; }
        public int EventId { get; private set; }

        public LinkAppointmentToEventCommand(int appointmemtId, int eventId)
        {
            AppointmentId = appointmemtId;
            EventId = eventId;
        }
    }
}