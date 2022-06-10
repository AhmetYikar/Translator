using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Domain.Entities
{


    public class LeetSpeakTranslation : BaseEntity
    {
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Deleted At")]
        public DateTime? DeletedAt { get; set; }
        public string DeletedById { get; set; }
        public ApplicationUser DeletedBy { get; set; }
        public string Text { get; set; }
        public string Translated { get; set; }
    }
}
