using BAL.Services.MessageService;
using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.UserGroupService
{
    public interface IUserGroupService
    {
        bool FindGroupUsers(Guid groupId, string userId);
        IEnumerable<UserGroup> GetUsersByGroupId(Guid groupId);
        IEnumerable<UserGroup> GetGroupUsers(Guid groupId);
    }
}
