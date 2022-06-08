using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Domain.Entities;

namespace Translator.Persistence.Context
{
    public class TranslationDbContext : IdentityDbContext
    {
        public TranslationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<LeetSpeakTranslation> LeetSpeakTranslations { get; set; }
    }
}
