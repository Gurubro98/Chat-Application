using BAL.Services.UnitOfWork;
using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.UserGroupService
{
    public class UserGroupService : IUserGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserGroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<UserGroup> GetUsersByGroupId(Guid groupId)
        {
            var users = _unitOfWork.Repository<UserGroup>().GetAll().Where(u => u.GroupId == groupId).ToList();
            return users;
        }

        public bool FindGroupUsers(Guid groupId, string userId)
        {
            var users = _unitOfWork.Repository<UserGroup>().GetAll().Where(u => u.GroupId == groupId && u.UserId == userId).ToList();
            if (users.Count == 0)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<UserGroup> GetGroupUsers(Guid groupId)
        {
            var users = _unitOfWork.Repository<UserGroup>().GetAll().Where(u => u.GroupId == groupId).ToList();
            return users;
        }
    }
}
