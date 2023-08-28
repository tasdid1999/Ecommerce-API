using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Model.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public long Amount { get; set; }

        public string? ProductDetails { get; set; }

        public string? ImageUrl { get; set; }

        public IFormFile? ImageFile { get; set; }

        public int catagoryId { get; set; }
    }
}
