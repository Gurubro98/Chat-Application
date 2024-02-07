using DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Request
    {
        public Guid RequestId { get; set; }
        public string SenderId { get; set; }
        public virtual User? Sender { get; set; }
        public string ReceiverId { get; set; }
        public virtual User? Receiver { get; set; }
        public RequestAction Status { get; set; }
        public bool IsTakeAction { get; set; }
        public bool IsDeleted { get; set; }
    }
}
