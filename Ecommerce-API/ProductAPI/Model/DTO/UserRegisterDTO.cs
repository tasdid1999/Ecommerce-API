using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Model.DTO
{
    public class UserRegisterDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }


    }
}
