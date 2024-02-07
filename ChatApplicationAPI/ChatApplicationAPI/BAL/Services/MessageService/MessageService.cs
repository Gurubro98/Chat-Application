using BAL.Services.UnitOfWork;
using DAL.Models;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Message> GetAllMessages(string senderId, string receiverId, int page)
        {
            int pageSize = 10;
            var unreadMessages = _unitOfWork.Repository<Message>().GetAll().Where(u => ((u.ReceiverId == senderId && u.SenderId == receiverId)) && (u.IsRead != true)).OrderBy(t => t.TimeStamp).ToList();
            foreach(Message message in unreadMessages)
            {
                message.IsRead = true;
                _unitOfWork.Repository<Message>().Update(message.MessageId, message);
            }
            _unitOfWork.Save();
            var messages = _unitOfWork.Repository<Message>().GetAll().Where(u => (u.SenderId == senderId && u.ReceiverId == receiverId) || (u.SenderId == receiverId && u.ReceiverId == senderId)).OrderByDescending(t => t.TimeStamp).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //messages = messages.OrderBy(t => t.TimeStamp).ToList();

            return messages;
        }

        public IEnumerable<Message> GetAllGroupMessages(Guid groupId, int page)
        {
            int pageSize = 10;
            var unreadMessages = _unitOfWork.Repository<Message>().GetAll().Where(u => (u.IsRead != true) && u.GroupId == groupId).ToList();
            var messages = _unitOfWork.Repository<Message>().GetAll().Where(u => u.GroupId == groupId).OrderByDescending(t => t.TimeStamp).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return messages;
        }

        public IEnumerable<Message> GetAllUnReadMessages(string userId)
        {
            var messages = _unitOfWork.Repository<Message>().GetAll().Where(u => (u.SenderId != userId) && (!u.IsRead.Value)).OrderBy(t => t.TimeStamp).ToList();
            return messages;
        }

        public Message GetLastMessage(string senderId)
        {
            try
            {
                Message lastMessage = _unitOfWork.Repository<Message>().GetAll().Include(s=>s.Sender).Include(r => r.Receiver).Include(a => a.Attachment).Where(u => u.SenderId == senderId).OrderByDescending(m => m.TimeStamp).FirstOrDefault();
              
                return lastMessage;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Add(Message message)
        {
            try
            {

                if (message != null)
                {

                    _unitOfWork.Repository<Message>().Create(message);
                    _unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public void MessageSeen(Message message)
        {
            try
            {

                if (message != null)
                {

                    _unitOfWork.Repository<Message>().Update(message.MessageId, message);
                    _unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
