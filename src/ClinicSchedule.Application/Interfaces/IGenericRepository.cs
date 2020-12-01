using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace ClinicSchedule.Application
{
    public interface IGenericRepository<T> : IDisposable where T : class 
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        void Update(T entity);
    }
}