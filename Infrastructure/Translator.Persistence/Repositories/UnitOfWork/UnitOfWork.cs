using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Application.Repositories;
using Translator.Application.Repositories.UnitOfWork;
using Translator.Persistence.Context;

namespace Translator.Persistence.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TranslationDbContext _context;
        public UnitOfWork(TranslationDbContext context)
        {
            _context = context;

            LeetSpeakTranslationRead = new LeetSpeakTranslationReadRepository(_context);
            LeetSpeakTranslationWrite = new LeetSpeakTranslationWriteRepository(_context);
            LogRead = new LogReadRepository(_context);
        }

     

        public ILeetSpeakTranslationReadRepository LeetSpeakTranslationRead { get; private set; }
        public ILeetSpeakTranslationWriteRepository LeetSpeakTranslationWrite { get; private set; }
        public ILogReadRepository LogRead { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
