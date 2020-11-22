using ClinicSchedule.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ClinicSchedule.Application
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(IAppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<IEnumerable<Event>> GetAllNotLinkedEventsAsync()
        {
            return await DbSet
                .Where(e => e.AppointmentId == null)
                .ToListAsync();
        }

        public IEnumerable<EventDTO> ConvertAllToDTO(IEnumerable<Event> events)
        {
            return events
                .Select(e => new EventDTO
                    {
                        Id = e.Id,
                        DateTime = e.DateTime,
                        ServiceId = e.ServiceId,
                        AppointmentId = e.AppointmentId,
                    });
        }
    }
}

