using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ClinicSchedule.Application
{
    class LinkAppointmentToEventHandler : AsyncRequestHandler<LinkAppointmentToEventCommand>
    {
        private readonly IUnitOfWork _uow;

        public LinkAppointmentToEventHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected override async Task Handle(LinkAppointmentToEventCommand command, CancellationToken cancellationToken)
        {
            var appointment = await _uow.Appointments.GetByIdAsync(command.AppointmentId);

            if (appointment == null)
                throw new Exception();

            var evnt = await _uow.Events.GetByIdAsync(command.EventId);

            if (evnt == null)
                throw new Exception();

            if (evnt.AppointmentId != null)
                throw new Exception();

            if (appointment.ServiceId != evnt.ServiceId)
                throw new Exception();

            evnt.AppointmentId = command.AppointmentId;

            _uow.Events.Update(evnt);
            await _uow.SaveChangesAsync();
        }
    }
}