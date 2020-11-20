using ClinicSchedule.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ClinicSchedule.Infrastructure
{
    public class SearchEventsService : ISearchEventsService

    {
        public AppDbContext _appDbContext; 

        public SearchEventsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<AvailableDateEvents> GetAvailableDateEventsForAllPatientAppointments(int patientId)
        {

            // Все назначения пациента, не привязанные к расписанию
            var appointments = _appDbContext.Appointment
                .Where(a => a.PatientId == patientId & a.Events.Count == 0); // (1)

            // Услуги из всех назначений пациента, не привязанных к расписанию. Запрос к БД
            var services = await appointments
                .Select(a => a.ServiceId)
                .ToListAsync();

            // Все незанятые ячейки. Запрос к БД
            var events = await _appDbContext.Event
                .Where(e => e.AppointmentId == null) // (2)
                .ToListAsync();

            // Группируем ячейки по группам, в качестве ключа группировки - дата без времени
            // Отбираем группы, в ячейках которых имеются все сервисы из списка services

            var groups = events
                .GroupBy(e => e.DateTime.Date, e => e)
                .Where(g => services.All(s => g.Select(e => e.ServiceId).Contains(s)));

            // Сортируем группы по дате
            // Берем первую или null, если последовательность групп пустая
            var group = groups
                .OrderBy(g => g.Key)
                .FirstOrDefault();

            // Проекция в DTO
            AvailableDateEvents availableDateEvents = new AvailableDateEvents();
//            {
//                AvailableDate = group?.Key,
//                AvailableEventsList = group?.ToList(),         
//            };

            return availableDateEvents;

            // (1) Если по условиям задачи требуется показать все назначения (привязанные и нет),
            // нужно из условия убрать выражение & a.Events.Count == 0

            // (2) Если по условиям задачи ячейка, занятая пациентом, является для этого пациента доступной,
            // нужно в условие добавить выражение || e.Appointment.PatientId == patientId
        }
    }
}

