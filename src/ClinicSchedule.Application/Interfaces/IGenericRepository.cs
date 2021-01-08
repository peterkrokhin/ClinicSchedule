using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClinicSchedule.Application
{
    public interface IGenericRepository<T> : IDisposable where T : class 
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> Find(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindMany(Expression<Func<T, bool>> predicate);
        void Update(T entity);
    }
}