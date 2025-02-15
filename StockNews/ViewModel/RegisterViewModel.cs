using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace StockNews.ViewModel
{
    public class RegisterViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage ="UserName is Required")]
        [StringLength(100,ErrorMessage ="Username Cannot be more than 100 Characters")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(100, ErrorMessage ="Password Cannot be more than 100 Characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage ="Email Id is Required")]
        [StringLength(256,ErrorMessage = "Emaild Cannot be more than 256 Characters")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

    }
}
