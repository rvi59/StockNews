using System.ComponentModel.DataAnnotations;

namespace StockNews.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(256)] 
        public string Email { get; set; } = string.Empty;


        public ICollection<UserCompany> UsrCompanies { get; set; }


    }
}
