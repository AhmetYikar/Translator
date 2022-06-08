using Translator.Application.Repositories;
using Translator.Domain.Entities;
using Translator.Persistence.Context;

namespace Translator.Persistence.Repositories
{
    public class LeetSpeakTranslationWriteRepository : WriteRepository<LeetSpeakTranslation>, ILeetSpeakTranslationWriteRepository
    {
        public LeetSpeakTranslationWriteRepository(TranslationDbContext context) : base(context)
        {

        }
    }
}
