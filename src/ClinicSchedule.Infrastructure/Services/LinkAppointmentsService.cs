using ClinicSchedule.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ClinicSchedule.Infrastructure
{
    public class LinkAppointmentsService : ILinkAppointmentsService
    {
        public AppDbContext _appDbContext; 

        public LinkAppointmentsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<string> LinkAppointmentToEvent(int appointmentId, int eventId)
        {
            // Запрос в БД
            var appointment = await _appDbContext.Appointment
                .Include(a => a.Events)
                .FirstOrDefaultAsync(a => a.Id == appointmentId);

            // Назначение не найдено
            if (appointment == null)
                return $"no Appointment by id={appointmentId}";

            // Назначение уже привязано
            if (appointment.Events.Count != 0)
                return $"Appointment id={appointmentId} is already linked";

            // Запрос в БД
            var evnt = await _appDbContext.Event
                .FirstOrDefaultAsync(e => e.Id == eventId);

            // Ячейка не найдена
            if (evnt == null)
                return $"no Event by id={eventId}";

            // Дополнительно проверяем, чтобы услуга в Appointment и услуга в Event совпали
            if (appointment.ServiceId != evnt.ServiceId)
                return $"service in Appointment id={appointmentId} and service in Event id={eventId} dont match";

            // Все Ок, апдейтим ячейку
            evnt.AppointmentId = appointmentId;

            _appDbContext.Event.Update(evnt);
            await _appDbContext.SaveChangesAsync();

            return "done";
        }
    }
}