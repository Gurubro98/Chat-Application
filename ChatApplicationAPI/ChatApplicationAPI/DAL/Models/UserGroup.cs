using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserGroup
    {
        public string UserId { get; set; }
        public virtual User? User { get; set; }
        public Guid GroupId { get; set; }
        public virtual GroupChat? Group { get; set; }
    }
}
