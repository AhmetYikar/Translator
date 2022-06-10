using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Translator.Web.Models
{
    public class TextViewModel
    {
        [RegularExpression(@"^[^<><|>]+$", ErrorMessage = "No html tags allowed!")]
        [Required()]
        [MaxLength(1024)]
        public string Text { get; set; }
    }
}
