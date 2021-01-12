using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ClinicSchedule.Application
{
    class LinkAppointmentToEventHandler : AsyncRequestHandler<LinkAppointmentToEventCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IAppointmentRepository _appointments;
        private readonly IEventRepository _events;

        public LinkAppointmentToEventHandler(IUnitOfWork uow, IAppointmentRepository appointments, 
            IEventRepository events)
        {
            _uow = uow;
            _appointments = appointments;
            _events = events;
        }

        protected override async Task Handle(LinkAppointmentToEventCommand command, CancellationToken cancellationToken)
        {
            var appointment = await _appointments.GetByIdAsync(command.AppointmentId);

            if (appointment == null)
                throw new AppointmentNotFoundException($"Назначение с id={command.AppointmentId} не найдено.");

            var evnt = await _events.GetByIdAsync(command.EventId);

            if (evnt == null)
                throw new EventNotFoundException($"Событие с id={command.EventId} не найдено.");

            if (evnt.AppointmentId != null)
                throw new EventNotAvailableException($"Событие с id={evnt.Id} уже занято.");

            // Пересмотреть
            if (await _events.Find(command.GetEventPredicate()) != null)
                throw new AppointmentAlreadyLinkedException($"Назначение с id={command.AppointmentId} уже привязано.");

            if (appointment.ServiceId != evnt.ServiceId)
                throw new ServicesAppointmentEventDontMatchException($"Id услуги в назначении и в событии не совпадают.");

            evnt.AppointmentId = command.AppointmentId;

            _events.Update(evnt);
            await _uow.SaveChangesAsync();
        }
    }
}