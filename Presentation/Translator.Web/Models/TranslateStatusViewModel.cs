using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Translator.Web.Models
{
    public class TranslateStatusViewModel
    {
        public bool Success { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public string Text { get; set; }
        public string Translated { get; set; }

    }
}
