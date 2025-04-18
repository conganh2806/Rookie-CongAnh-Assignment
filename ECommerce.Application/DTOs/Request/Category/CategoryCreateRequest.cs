using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{
    public class CategoryCreateRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; } = default!;    
        [Required(ErrorMessage = "Slug is required")]
        [StringLength(100, ErrorMessage = "Slug cannot be longer than 100 characters")]
        public string Slug { get; set; } = default!;

        public string? ParentId { get; set; }
    }
}