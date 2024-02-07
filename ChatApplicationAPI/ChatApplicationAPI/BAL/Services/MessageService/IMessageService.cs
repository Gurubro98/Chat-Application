using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.MessageService
{
    public interface IMessageService
    {
        IEnumerable<Message> GetAllMessages(string senderId, string receiverId, int page);
        IEnumerable<Message> GetAllGroupMessages(Guid groupId, int page);
        Message GetLastMessage(string senderId);
        IEnumerable<Message> GetAllUnReadMessages(string userId);
        void Add(Message message);
        void MessageSeen(Message message);
    }
}
