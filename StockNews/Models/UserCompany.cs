using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockNews.Models
{
    public class UserCompany
    {

        [Key]
        public int Id { get; set; }

        // Foreign key for User
        public int UserId { get; set; }

        // Navigation property for User
        [ForeignKey("UserId")]
        public User User { get; set; }

        // Foreign key for Company
        public int CompanyId { get; set; }

        // Navigation property for Company
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

    }
}
