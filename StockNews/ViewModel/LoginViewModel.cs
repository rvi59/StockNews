using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace StockNews.ViewModel
{
    public class LoginViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        

        [Required(ErrorMessage = "EmailId is Required")]
        [StringLength(256, ErrorMessage = "Emaild Cannot be more than 256 Characters")]
        [DataType(DataType.EmailAddress)]
        public string EmailId{ get; set; } = string.Empty;


        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

    }
}
