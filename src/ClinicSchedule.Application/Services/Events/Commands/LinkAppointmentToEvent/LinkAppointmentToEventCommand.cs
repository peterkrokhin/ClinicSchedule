using MediatR;

namespace ClinicSchedule.Application
{
    public class LinkAppointmentToEventCommand : IRequest<int>
    {
        private readonly int _appointmentId;
        private readonly int _eventId;

        public LinkAppointmentToEventCommand(int appointmemtId, int eventId)
        {
            _appointmentId = appointmemtId;
            _eventId = eventId;
        }
    }
}