using Translator.Application.Repositories;
using Translator.Domain.Entities;
using Translator.Persistence.Context;

namespace Translator.Persistence.Repositories
{
    public class LogReadRepository : ReadRepository<Log>, ILogReadRepository
    {
        public LogReadRepository(TranslationDbContext context) : base(context)
        {

        }
    }
}
