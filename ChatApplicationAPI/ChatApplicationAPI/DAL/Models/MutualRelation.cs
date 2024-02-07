using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MutualRelation
    {
        public string MutualId { get; set; }
        public virtual User? Mutual { get; set; }
        public string UserId { get; set; }
        public virtual User? User { get; set;}
        public bool IsDeleted { get; set; } 
    }
}
