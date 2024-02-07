using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelDTO
{
    public class Register
    {
        [Required(ErrorMessage = "Name is Required")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$",
           ErrorMessage = "Name cann't contain extra space, Special Characters, and numeric Values")]
        [MinLength(3, ErrorMessage = "Name has atleast 3 Charcaters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
    ErrorMessage = "Please Enter a Valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{7,}",
    ErrorMessage = "Password must be Minimum 7 characters and contain at least one number, one uppercase letter, one lower case letter and Special Character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public bool IsSendRequest { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ProfilePic { get; set; }
    }
}
