using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Application.Repositories;
using Translator.Domain.Entities;
using Translator.Persistence.Context;

namespace Translator.Persistence.Repositories
{
    public class LeetSpeakTranslationReadRepository:ReadRepository<LeetSpeakTranslation>, ILeetSpeakTranslationReadRepository
    {
        public LeetSpeakTranslationReadRepository(TranslationDbContext context):base(context)
        {

        }
    }
}
