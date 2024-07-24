using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Persistence.Contracts;

namespace Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task<T> GetByIdAsync(int id)
        {
            var result = _context.Set<T>().Find(id);
            return Task.FromResult(result);
        }

        public Task<List<T>> ListAllAsync()
        {
            var result = _context.Set<T>().ToList();
            return Task.FromResult(result);
        }

        public Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}

