using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Model.Domain
{
    public class ShoppingCart
    {

        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }


    }
}
