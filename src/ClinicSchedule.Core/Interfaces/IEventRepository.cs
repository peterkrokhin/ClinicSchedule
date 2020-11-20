using System.Threading.Tasks;
using System.Collections.Generic;

namespace ClinicSchedule.Core
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<IEnumerable<Event>> GetAllNotLinkedEventsAsync();
        IEnumerable<EventDTO> ConvertAllToDTO(IEnumerable<Event> events);
    }
}