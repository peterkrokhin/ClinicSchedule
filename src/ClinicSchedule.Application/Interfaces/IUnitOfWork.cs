using System;
using System.Threading.Tasks;

namespace ClinicSchedule.Application
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();
    }
}