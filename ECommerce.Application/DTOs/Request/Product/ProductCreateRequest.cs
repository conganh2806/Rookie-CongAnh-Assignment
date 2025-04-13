using System.ComponentModel.DataAnnotations;
using ECommerce.Domain.Entities.Enum;

namespace ECommerce.Application.DTOs
{
    public class ProductCreateRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Discount is required.")]
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100 percent.")]
        public float Discount { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Sold amount is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Sold amount cannot be negative.")]
        public int Sold { get; set; } 

        [EnumDataType(typeof(ProductStatus), ErrorMessage = "Invalid product status.")]
        public ProductStatus ProductStatus { get; set; }
        
        public List<string> CategoryIds { get; set; } = new List<string>();
        public string Slug { get; set; } = default!;
        public string ImageURL { get; set; } = default!;
    }
}