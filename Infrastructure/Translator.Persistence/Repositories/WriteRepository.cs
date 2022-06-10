using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Translator.Application.Repositories;
using Translator.Domain.Entities;
using Translator.Persistence.Context;

namespace Translator.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly TranslationDbContext _context;
        public WriteRepository(TranslationDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        DbSet<T> IRepository<T>.Table => throw new NotImplementedException();

        async Task<bool> IWriteRepository<T>.AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        async Task<bool> IWriteRepository<T>.AddRangeAsync(List<T> datas)
        {
            await Table.AddRangeAsync(datas);
            return true;
        }
        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }
        public bool RemoveRange(List<T> datas)
        {
            Table.RemoveRange(datas);
            return true;
        }

        bool IWriteRepository<T>.Update(T model)
        {
            EntityEntry<T> entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }       

        //Save changes
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
      
    }

}
