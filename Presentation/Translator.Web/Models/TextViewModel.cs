using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Translator.Web.Models
{
    public class TextViewModel
    {
        [RegularExpression(@"<[^>]*>", ErrorMessage = "No html tags allowed!")]
        [Required(ErrorMessage = "Text cannot exceed 1024 characters")]
        [MaxLength(1024, ErrorMessage = "Text is very long!")]
        public string Text { get; set; }
    }
}
