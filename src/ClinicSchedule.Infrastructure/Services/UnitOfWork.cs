using System;
using ClinicSchedule.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ClinicSchedule.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext DbContext { get; set; }
        public IAppointmentRepository Appointments { get; set; }
        public IEventRepository Events{ get; set; }

        public UnitOfWork(AppDbContext appDbContext)
        {
            DbContext = appDbContext;
            Appointments = new AppointmentRepository(DbContext);
            Events = new EventRepository(DbContext);
        }

        public async Task<AvailableDateEvents> GetAvailableDateEventsForAllPatientAppointmentsAsync(int patientId)
        {
            // Все назначения пациента, не привязанные к расписанию, из БД
            var appointments = await Appointments.GetNotLinkedAppointmentsByPatientIdAsync(patientId);

            if (appointments.Count() == 0)
                throw new Exception($"appointmetns by id={patientId} not found");

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
                AvailableEventsList = group != null ? Events.ConvertAllToDTO(group.ToList()) : null,        
            };
        }

        public async Task<string> LinkAppointmentToEventAsync(int appointmentId, int eventId)
        {
            // Запрашиваем из БД назначение по id
            var appointment = await Appointments.GetByIdIncludeEventsAsync(appointmentId);

            // Назначение не найдено
            if (appointment == null)
                return $"no Appointment by id={appointmentId}";

            // Назначение уже привязано
            if (appointment.Events.Count != 0)
                return $"Appointment id={appointmentId} is already linked";

            // Запрашиваем из БД ячейку по id
            var evnt = await Events.GetByIdAsync(eventId);

            // Ячейка не найдена
            if (evnt == null)
                return $"no Event by id={eventId}";

            // Дополнительно проверяем, чтобы услуга в Appointment и услуга в Event совпали
            if (appointment.ServiceId != evnt.ServiceId)
                return $"service in Appointment id={appointmentId} and service in Event id={eventId} dont match";

            // Все Ок, апдейтим ячейку
            evnt.AppointmentId = appointmentId;

            Events.Update(evnt);
            await SaveChangesAsync();

            return "done";
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

