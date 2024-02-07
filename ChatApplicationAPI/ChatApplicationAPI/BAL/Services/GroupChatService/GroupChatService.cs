using BAL.Services.MessageService;
using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.GroupChatService
{
    public class GroupChatService : IGroupChatService
    {
        private readonly IGenericRepository<GroupChat> _genericGroupRepoisitory;
        public GroupChatService(IGenericRepository<GroupChat> genericChatRepoisitory)
        {
            _genericGroupRepoisitory = genericChatRepoisitory;
        }

        public void Add(GroupChat model)
        {
            _genericGroupRepoisitory.Create(model);
            _genericGroupRepoisitory.Save();
        }

        public GroupChat FindGroup(string groupName)
        {
            return _genericGroupRepoisitory.GetAll().Where(g => g.GroupName == groupName).FirstOrDefault();
        }
    }
}
