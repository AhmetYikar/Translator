using System.Collections.Generic;
using System.Threading.Tasks;
using Translator.Domain.Entities;

namespace Translator.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> datas);
        bool Remove(T model);
        bool RemoveRange(List<T> datas);
        bool Update(T model);
        bool Delete(T model);

        int SaveChanges(T model);
        Task<int> SaveChangesAsync(T model);
    }
}
