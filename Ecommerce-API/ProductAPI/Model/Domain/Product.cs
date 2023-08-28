using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAPI.Model.Domain
{
    public class Product
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

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public int catagoryId { get; set; }

        public virtual Catagory Catagory { get; set; }
    }
}
