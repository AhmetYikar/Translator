using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Translator.Domain.Entities;

namespace Translator.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method);

        T GetSingle(Expression<Func<T, bool>> method);
        T GetById(int id);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method);
        Task<T> GetByIdAsync(int id);
       
    }
}
