using System.ComponentModel.DataAnnotations;

namespace StockNews.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }


        [Required]
        [StringLength(250)]
        public string CompanyName { get; set; } = string.Empty;


        public ICollection<UserCompany> UsrCompanies { get; set; }
    }
}
