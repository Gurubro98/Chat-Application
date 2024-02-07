using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DAL.ModelDTO
{
    public class MessageDTO
    {
        public Guid MessageId { get; set; }
        [Required(ErrorMessage = "Message is required")]
        public string? Content { get; set; }

        public string SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid? AttachmentId { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsDeleted { get; set; }
        public Guid? GroupId { get; set; }
    }
}
