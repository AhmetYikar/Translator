using Microsoft.EntityFrameworkCore;
using System.Text;
using Translator.Domain.Entities;

namespace Translator.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
