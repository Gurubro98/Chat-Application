using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.GroupChatService
{
    public interface IGroupChatService
    {
        void Add(GroupChat model);
        GroupChat FindGroup(string groupName);
    }
}
