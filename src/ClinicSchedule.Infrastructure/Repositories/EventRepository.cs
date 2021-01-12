using ClinicSchedule.Core;
using ClinicSchedule.Application;

namespace ClinicSchedule.Infrastructure
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(IAppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}

