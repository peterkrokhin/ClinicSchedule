using ClinicSchedule.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClinicSchedule.Infrastructure
{
    abstract public class GenericRepository<T> : IGenericRepository<T> where T:class
    {
        public AppDbContext DbContext { get; set; }
        public DbSet<T> DbSet { get; set; }

        public GenericRepository(AppDbContext appDbContext)
        {
            DbContext = appDbContext;
            DbSet = appDbContext.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }
        
    }
}