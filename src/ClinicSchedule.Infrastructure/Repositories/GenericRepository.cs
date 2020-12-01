using ClinicSchedule.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using ClinicSchedule.Application;

namespace ClinicSchedule.Infrastructure
{
    abstract public class GenericRepository<T> : IGenericRepository<T> where T:class
    {
        public IAppDbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public GenericRepository(IAppDbContext appDbContext)
        {
            DbContext = appDbContext;
            DbSet = appDbContext.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if(disposed)
                return;

            if(disposing)
            {
                DbContext?.Dispose();
                // Console.WriteLine($"object {this.ToString()} Dispose"); // Проверка работы Dispose()
            }
            
            disposed = true;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}