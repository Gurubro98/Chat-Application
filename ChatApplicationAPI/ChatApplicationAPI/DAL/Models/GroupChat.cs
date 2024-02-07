using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class GroupChat
    {
        [Key]
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public string UserId { get; set; }
        public virtual User? User { get; set; }
        public bool? IsDeleted { get; set; }    
    }
}
