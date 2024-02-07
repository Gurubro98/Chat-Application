using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Message
    {
        public Guid MessageId { get; set; }
        [Required(ErrorMessage ="Message is required")]
        public string? Content { get; set; }
       
        public string SenderId { get; set; }
        public virtual User? Sender { get; set; }
        public string? ReceiverId { get; set; }
        public virtual User? Receiver { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsRead { get; set; }

        public Guid? GroupId { get; set; }

        public virtual GroupChat? Group { get; set; }
        public Guid? AttachmentId { get; set; }
        public virtual Attachment? Attachment { get; set; }

    }
}
