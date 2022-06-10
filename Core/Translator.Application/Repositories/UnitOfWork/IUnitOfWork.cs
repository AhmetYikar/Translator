using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Application.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public ILeetSpeakTranslationReadRepository LeetSpeakTranslationRead { get; }
        public ILeetSpeakTranslationWriteRepository LeetSpeakTranslationWrite { get; }
        public ILogReadRepository LogRead { get; }

    }


}
