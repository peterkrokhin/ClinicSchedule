using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace ClinicSchedule.Application
{
    public class QuerryAggregator : IQuerryAggregator
    {
        private IUnitOfWork UnitOfWork { get; set; }
        public QuerryAggregator(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<DateEvents> GetAvailableDateEventsForAllPatientAppointmentsAsync(int patientId)
        {
            // Все назначения пациента, не привязанные к расписанию, из БД
            var appointments = await UnitOfWork.Appointments.GetNotLinkedAppointmentsByPatientIdAsync(patientId);

            // Назначения не найдены
            if (appointments.Count() == 0)
                return DateEvents.CreateEmpty();
          
            // Все незанятые ячейки из БД
            var events = await UnitOfWork.Events.GetAllNotLinkedEventsAsync();

            // Группируем ячейки по группам, в качестве ключа группировки - дата без времени
            // Отбираем группы, в ячейках которых имеются все услуги из всех назначений пациента
            var groups = events
                .GroupBy(e => e.DateTime.Date, e => e)
                .Where(g => appointments.Select(a => a.ServiceId)
                    .All(s => g.Select(e => e.ServiceId).Contains(s)));

            // Сортируем группы по дате
            // Берем первую или null, если последовательность групп пустая
            var group = groups
                .OrderBy(g => g.Key)
                .FirstOrDefault();

            // Проекция в DTO
            return new DateEvents()
            {
                Date = group?.Key,
                EventList = UnitOfWork.Events?.ConvertAllToDTO(group.ToList()) ?? new List<EventDTO>(),
            };
        }

        public async Task TryLinkAppointmentToEventAsync(int appointmentId, int eventId)
        {
            // Запрашиваем из БД назначение по id
            var appointment = await UnitOfWork.Appointments.GetByIdIncludeEventsAsync(appointmentId);

            // Назначение не найдено
            if (appointment == null)
                throw new Exception($"no Appointment by id={appointmentId}");

            // Назначение уже привязано
            if (appointment.Events.Count != 0)
                throw new Exception($"Appointment id={appointmentId} is already linked");

            // Запрашиваем из БД ячейку по id
            var evnt = await UnitOfWork.Events.GetByIdAsync(eventId);

            // Ячейка не найдена
            if (evnt == null)
                throw new Exception($"no Event by id={eventId}");

            // Дополнительно проверяем, чтобы услуга в Appointment и услуга в Event совпали
            if (appointment.ServiceId != evnt.ServiceId)
                throw new Exception($"service in Appointment id={appointmentId} and service in Event id={eventId} dont match");

            // Все Ок, апдейтим ячейку
            evnt.AppointmentId = appointmentId;

            UnitOfWork.Events.Update(evnt);
            await UnitOfWork.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if(disposed)
                return;

            if(disposing)
            {
                UnitOfWork?.Dispose();
                // Console.WriteLine($"object {this.ToString()} Dispose"); // Проверка работы Dispose()
            }
            
            disposed = true;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}