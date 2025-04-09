using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
        public bool IsDeleted { get; set; } = false;
    }
}