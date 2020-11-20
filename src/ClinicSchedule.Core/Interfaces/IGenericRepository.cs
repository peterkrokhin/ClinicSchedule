using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace ClinicSchedule.Core
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        void Update(T entity);
    }
}