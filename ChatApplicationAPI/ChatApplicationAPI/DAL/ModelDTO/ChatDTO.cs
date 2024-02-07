using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelDTO
{
    public class ChatDTO
    {
        public Guid ChatId { get; set; }
        public string UserId { get; set; }
        public string ParticipentId { get; set; }
        public Boolean? IsDeleted { get; set; }
    }
}
