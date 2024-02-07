using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [NotMapped]
        public bool IsSendRequest { get; set; }
        public string? ImageUrl {get; set;}
        [NotMapped]
        public IFormFile? ProfilePic { get; set;} 
        
        
    }
}
