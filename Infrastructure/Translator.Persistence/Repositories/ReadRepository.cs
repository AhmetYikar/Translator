using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Translator.Application.Repositories;
using Translator.Domain.Entities;
using Translator.Persistence.Context;

namespace Translator.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly TranslationDbContext _context;
        public ReadRepository(TranslationDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();




        public IQueryable<T> GetAll()
        {
            return Table.AsNoTracking();
        }


        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
        {
            return Table.Where(method).AsNoTracking();
        }



        public  T GetById(int id)
        {
            return  Table.Find(id);
        }


        public async Task<T> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }


        public T GetSingle(Expression<Func<T, bool>> method)
        {
            return Table.FirstOrDefault(method);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
        {
            return await Table.FirstOrDefaultAsync(method);
        }


    }

}
