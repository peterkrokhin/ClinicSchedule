using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ClinicSchedule.Core;

namespace ClinicSchedule.Application
{
    public class FindSuitableDateHandler : IRequestHandler<FindSuitableDateQuery, FindSuitableDateResponse>
    {
        private IAppointmentRepository _appointments;
        private IEventRepository _events;
        private IMapper _mapper;

        public FindSuitableDateHandler(IAppointmentRepository appointments, IEventRepository events, IMapper mapper)
        {
            _appointments = appointments;
            _events = events;
            _mapper = mapper;
        }

        public async Task<FindSuitableDateResponse> Handle(FindSuitableDateQuery query, CancellationToken cancellationToken)
        {
            // Все назначения пациента, не привязанные к расписанию, из БД
            var appointments = await _appointments.FindMany(query.GetAppointmentPredicate());

            // Назначения не найдены
            if (appointments.Count() == 0)
                return FindSuitableDateResponse.CreateEmpty();

            // Все незанятые ячейки из БД
            var events = await _events.FindMany(query.GetEventPredicate());

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
            return new FindSuitableDateResponse()
            {
                Date = group?.Key.Date,
                EventList = group?.ToList() != null ? 
                    _mapper.Map<IEnumerable<Event>, IEnumerable<SuitableEvent>>(group.ToList()) : 
                    new List<SuitableEvent>(),
            };
                

        }
    }
}