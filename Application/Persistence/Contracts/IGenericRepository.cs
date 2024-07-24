using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Persistence.Contracts
{
    public interface IGenericRepository
    {
        Task<T> AddAsync<T>(T entity);
        Task DeleteAsync<T>(T entity);
        Task<T> GetByIdAsync<T>(int id);
        Task<IReadOnlyList<T>> ListAllAsync<T>();
        Task UpdateAsync<T>(T entity);

    }
}