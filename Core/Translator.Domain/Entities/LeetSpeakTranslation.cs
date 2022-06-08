using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Domain.Entities
{


    public class LeetSpeakTranslation : BaseEntity
    {
        public string Text { get; set; }
        public string Translated { get; set; }
    }
}
