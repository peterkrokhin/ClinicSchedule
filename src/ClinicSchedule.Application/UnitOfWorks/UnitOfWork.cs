using System;
using ClinicSchedule.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ClinicSchedule.Application
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAppDbContext DbContext { get; set; }
        public IAppointmentRepository Appointments { get; set; }
        public IEventRepository Events{ get; set; }

        public UnitOfWork(IAppDbContext appDbContext)
        {
            DbContext = appDbContext;
            Appointments = new AppointmentRepository(DbContext);
            Events = new EventRepository(DbContext);
        }

        public async Task<AvailableDateEvents> GetAvailableDateEventsForAllPatientAppointmentsAsync(int patientId)
        {
            // Все назначения пациента, не привязанные к расписанию, из БД
            var appointments = await Appointments.GetNotLinkedAppointmentsByPatientIdAsync(patientId);

            // Назначения не найдены
            if (appointments.Count() == 0)
                return AvailableDateEvents.CreateEmpty();
          
            // Все незанятые ячейки из БД
            var events = await Events.GetAllNotLinkedEventsAsync();

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
            return new AvailableDateEvents()
            {
                AvailableDate = group?.Key,
                AvailableEventsList = Events?.ConvertAllToDTO(group.ToList()) ?? new List<EventDTO>(),
            };
  
        }

        public async Task TryLinkAppointmentToEventAsync(int appointmentId, int eventId)
        {
            // Запрашиваем из БД назначение по id
            var appointment = await Appointments.GetByIdIncludeEventsAsync(appointmentId);

            // Назначение не найдено
            if (appointment == null)
                throw new Exception($"no Appointment by id={appointmentId}");

            // Назначение уже привязано
            if (appointment.Events.Count != 0)
                throw new Exception($"Appointment id={appointmentId} is already linked");

            // Запрашиваем из БД ячейку по id
            var evnt = await Events.GetByIdAsync(eventId);

            // Ячейка не найдена
            if (evnt == null)
                throw new Exception($"no Event by id={eventId}");

            // Дополнительно проверяем, чтобы услуга в Appointment и услуга в Event совпали
            if (appointment.ServiceId != evnt.ServiceId)
                throw new Exception($"service in Appointment id={appointmentId} and service in Event id={eventId} dont match");

            // Все Ок, апдейтим ячейку
            evnt.AppointmentId = appointmentId;

            Events.Update(evnt);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if(disposing)
                {
                    DbContext.Dispose();
                    // Console.WriteLine($"object {this.ToString()} Dispose"); // Проверка работы Dispose()
                }
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

