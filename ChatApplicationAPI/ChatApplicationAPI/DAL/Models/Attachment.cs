using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Attachment
    {
        public Guid AttachmentId { get; set; }
        [NotMapped]
        public IFormFile? AttachmentFile { get; set; }
        public string? FileName { get; set; }
        public DateTime createdTime { get; set; }
    }
}
