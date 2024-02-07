using AutoMapper;
using BAL.Services.GroupChatService;
using BAL.Services.MessageService;
using BAL.Services.UnitOfWork;
using DAL.ModelDTO;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace ChatApplicationAPI.Hub
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IMessageService _messageService;
        private readonly IGroupChatService _groupService;
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ChatHub(IMessageService message, IMapper mapper, IUnitOfWork unitOfWork, IGroupChatService groupService, IWebHostEnvironment webHostEnvironment)
        {
            _messageService = message;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _groupService = groupService;
            _webHostEnvironment = webHostEnvironment;
        }

        static IList<Connection> Users = new List<Connection>();

        public override async Task OnConnectedAsync()
        {

            string userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string loggedInUserName = Context.User.FindFirstValue("Name");

            var existingUser = Users.FirstOrDefault(x => x.UserId == userId);
            var indexExistingUser = Users.IndexOf(existingUser);

            Connection user = new Connection
            {
                UserId = userId,
                ConnectionId = Context.ConnectionId
            };

            if (!Users.Contains(existingUser))
            {
                Users.Add(user);
            }
            else
            {
                Users[indexExistingUser] = user;
            }
            await Clients.AllExcept(user.ConnectionId).SendAsync("ReceiveNotification", null, loggedInUserName + " Online now");
            await Clients.All.SendAsync("BroadcastUserOnConnect", Users);

            await base.OnConnectedAsync();
        }

        public async Task CreateGroup(GroupChat model)
        {
            _groupService.Add(model);
            var group = _groupService.FindGroup(model.GroupName);
            UserGroup userGroup = new UserGroup
            {
                GroupId = group.GroupId,
                UserId = model.UserId,
            };
            _unitOfWork.Repository<UserGroup>().Create(userGroup);
            _unitOfWork.Save();
            await Clients.All.SendAsync("NewGroup", group);
        }

        public async Task UserRemoveFromGroup(Guid groupId, string userId)
        {
            var userFromGroup = _unitOfWork.Repository<UserGroup>().GetAll().Where(u => u.GroupId == groupId && u.UserId == userId).FirstOrDefault();
            _unitOfWork.Repository<UserGroup>().DeleteObject(userFromGroup);
            _unitOfWork.Save();
            await Clients.All.SendAsync("RemoveFromGroup", groupId, userId);
        }

        public async Task JoinGroup(UserGroup model)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, model.GroupId.ToString());
        }


        public async Task SendMessageToUser(MessageDTO messageModel)
        {
            if(messageModel.Content == null || messageModel.Content == "")
            {
                messageModel.Content = null;
            }
            var reciever = Users.FirstOrDefault(x => x.UserId == messageModel.ReceiverId);
            var connectionId = reciever == null ? "offlineUser" : reciever.ConnectionId;
            var message = _mapper.Map<Message>(messageModel);
            var msgDate = DateTime.Parse(messageModel.TimeStamp.ToString());
            messageModel.TimeStamp = msgDate.ToUniversalTime();
            messageModel.IsRead = false;
            _messageService.Add(message);

            Message lastMessage = _messageService.GetLastMessage(message.SenderId);
            string currentUserConnectionId = Users.Where(u => u.UserId == messageModel.SenderId).Select(c => c.ConnectionId).FirstOrDefault();
            if (messageModel.GroupId != null)
            {
                try
                {
                    var lastGroupMessage = _unitOfWork.Repository<Message>().GetAll().Include(g => g.Group).Include(s => s.Sender).Include(r => r.Receiver).Where(m => m.GroupId == message.GroupId && m.SenderId == message.SenderId).OrderByDescending(t => t.TimeStamp).FirstOrDefault();
                    //var isUserInGroup = _unitOfWork.Repository<UserGroup>().GetAll().Where(u=> u.UserId == lastGroupMessage.SenderId &&u.group)
                    await Clients.AllExcept(currentUserConnectionId).SendAsync("ReceiveNotification", lastGroupMessage.Group.GroupName, lastGroupMessage.Sender.Name + " " + message.Content, null);
                    await Clients.GroupExcept(messageModel.GroupId.ToString(), currentUserConnectionId)
                        .SendAsync("ReceiveMessage", lastMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                await Clients.Client(connectionId).SendAsync("ReceiveNotification", lastMessage.Sender.Name, message.Content, lastMessage.ReceiverId);
                await Clients.Client(connectionId).SendAsync("ReceiveDM", Context.ConnectionId, lastMessage);
            }
        }

        public async Task SendRequest(Request RequestModel)
        {
            try
            {
                var request = _unitOfWork.Repository<Request>().GetAll().Where(r => r.SenderId == RequestModel.SenderId && r.ReceiverId == RequestModel.ReceiverId && !r.IsDeleted).FirstOrDefault();
                if (request == null)
                {
                    var reciever = Users.FirstOrDefault(x => x.UserId == RequestModel.ReceiverId);
                    var connectionId = reciever == null ? "offlineUser" : reciever.ConnectionId;
                    _unitOfWork.Repository<Request>().Create(RequestModel);
                    _unitOfWork.Save();
                    await Clients.Client(connectionId).SendAsync("ReceiveRequest", RequestModel);
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public async Task TakeActionOnRequest(Request RequestModel)
        {
            try
            {
                var user = Users;
                MutualRelation mutualUser = new MutualRelation
                {
                    UserId = RequestModel.SenderId,
                    MutualId = RequestModel.ReceiverId
                };
                var requestReceiver = Users.FirstOrDefault(x => x.UserId == RequestModel.SenderId);
                var connectionId = requestReceiver == null ? "offlineUser" : requestReceiver.ConnectionId;
                if (RequestModel.Status == DAL.Enum.RequestAction.Rejected)
                {
                    RequestModel.IsDeleted = true;
                }
                _unitOfWork.Repository<Request>().Update(RequestModel.RequestId, RequestModel);
                if (RequestModel.Status == DAL.Enum.RequestAction.Approved)
                {
                    _unitOfWork.Repository<MutualRelation>().Create(mutualUser);
                }
                _unitOfWork.Save();
                var requestSender = Users.FirstOrDefault(x => x.UserId == RequestModel.ReceiverId);
                var senderConnectionId = requestSender == null ? "offlineUser" : requestSender.ConnectionId;
                var RelationalUser = _unitOfWork.Repository<MutualRelation>().GetAll()
                    .Include(u => u.User)
                    .Include(u => u.Mutual).Where(u => u.UserId == RequestModel.SenderId && u.MutualId == RequestModel.ReceiverId)
                    .FirstOrDefault();
                await Clients.Client(connectionId).SendAsync("FriendUser", RelationalUser);
                await Clients.Client(senderConnectionId).SendAsync("FriendUser", RelationalUser);
                

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public async Task RemoveUserFromList(string loggedInUserId, string removerUserId)
        {
            var loggedInUser = Users.FirstOrDefault(x => x.UserId == loggedInUserId);
            var loggedInUserrConnectionId = loggedInUser == null ? "offlineUser" : loggedInUser.ConnectionId;
            var RelationalUser = _unitOfWork.Repository<MutualRelation>().GetAll()
                    .Where(u => (u.UserId == loggedInUserId && u.MutualId == removerUserId) || (u.UserId == removerUserId && u.MutualId == loggedInUserId))
                    .FirstOrDefault();
            var request = _unitOfWork.Repository<Request>().GetAll().Where(u=> (u.SenderId == loggedInUserId && u.ReceiverId == removerUserId) || (u.SenderId == removerUserId && u.ReceiverId == loggedInUserId));
            foreach(var item in request)
            {
                item.IsDeleted = true;
                _unitOfWork.Repository<Request>().Update(item.RequestId,item);
            }
            
            _unitOfWork.Repository<MutualRelation>().DeleteObject(RelationalUser);
            _unitOfWork.Save();
            var removerUser = Users.FirstOrDefault(x => x.UserId == removerUserId);
            var removerUserrConnectionId = removerUser == null ? "offlineUser" : removerUser.ConnectionId;
            await Clients.Client(removerUserrConnectionId).SendAsync("RemoveUser", RelationalUser);
            await Clients.Client(loggedInUserrConnectionId).SendAsync("RemoveUser", RelationalUser);
        }


        public void RemoveOnlineUser(string userID)
        {
            var user = Users.Where(x => x.UserId == userID).ToList();
            foreach (Connection i in user)
                Users.Remove(i);

            Clients.All.SendAsync("BroadcastUserOnDisconnect", Users);
        }

        public override Task OnDisconnectedAsync(Exception? exp)
        {
            string userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = Users.Where(x => x.UserId == userId).ToList();
            foreach (Connection i in user)
                Users.Remove(i);

            Clients.All.SendAsync("BroadcastUserOnDisconnect", Users);
            return base.OnDisconnectedAsync(exp);
        }
    }
}
