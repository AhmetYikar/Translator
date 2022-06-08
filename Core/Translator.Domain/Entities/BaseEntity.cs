using System;

namespace Translator.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedById { get; set; }
        public ApplicationUser DeletedBy { get; set; }

    }
}
