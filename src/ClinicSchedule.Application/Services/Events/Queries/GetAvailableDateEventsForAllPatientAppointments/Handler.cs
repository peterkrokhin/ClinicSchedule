using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;

using ClinicSchedule.Core;

namespace ClinicSchedule.Application.Services.Events.Queries.GetAvailableDateEventsForAllPatientAppointments
{
    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IAppointmentRepository _appointments;
        private readonly IEventRepository _events;
        private readonly IMapper _mapper;

        public Handler(IAppointmentRepository appointments, IEventRepository events, IMapper mapper)
        {
            _appointments = appointments;
            _events = events;
            _mapper = mapper;
        }

        public async Task<Response> Handle(Query query, CancellationToken cancellationToken)
        {
            // Все назначения пациента, не привязанные к расписанию, из БД
            var appointments = await _appointments.GetNotLinkedAppointmentsByPatientIdAsync(query.Id);

            // Назначения не найдены
            if (appointments.Count() == 0)
                return Response.CreateEmpty();

            // Все незанятые ячейки из БД
            var events = await _events.GetAllNotLinkedEventsAsync();

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
            return new Response()
            {
                Date = group?.Key,
                EventList = group?.ToList() != null ? 
                    _mapper.Map<IEnumerable<Event>, IEnumerable<EventResponse>>(group.ToList()) : 
                    new List<EventResponse>(),
            };
        }
    }
}