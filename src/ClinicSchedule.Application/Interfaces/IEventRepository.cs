using System.Threading.Tasks;
using System.Collections.Generic;
using ClinicSchedule.Core;

namespace ClinicSchedule.Application
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<IEnumerable<Event>> GetAllNotLinkedEventsAsync();
        IEnumerable<EventDTO> ConvertAllToDTO(IEnumerable<Event> events);
    }
}