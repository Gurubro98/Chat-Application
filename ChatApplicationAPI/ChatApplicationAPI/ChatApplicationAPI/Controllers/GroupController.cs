using AutoMapper;
using BAL.Services.GroupChatService;
using BAL.Services.MessageService;
using BAL.Services.UnitOfWork;
using BAL.Services.UserGroupService;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplicationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupChatService _groupService;
        private readonly IUserGroupService _userGroupService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GroupController(IUnitOfWork unitOfWork, IGroupChatService groupService, IMapper mapper, IUserGroupService userGroupService)
        {
            //_groupRepository = groupRepository;
            _groupService = groupService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            //_userGroupRepository = userGroupRepository;
            _userGroupService = userGroupService;
            //_groupCHatModelRepository = groupCHatModelRepository;
        }

        [HttpGet]
        public IActionResult GetAllGroups()
        {
            try
            {
                var groups = _unitOfWork.Repository<GroupChat>().GetAll().ToList();
                return Ok(new { groups });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{groupId}")]
        public IActionResult GetGroupUsers(Guid groupId)
        {
            try
            {
                var groupUsers = _userGroupService.GetGroupUsers(groupId);
                return Ok(new { groupUsers });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult FindGroupUsers(Guid groupId, string userId)
        {
            try
            {
                bool IsUserAddedInGroup = _userGroupService.FindGroupUsers(groupId, userId);
                return Ok(new { IsUserAddedInGroup });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult JoinGroup(UserGroup model)
        {
            try
            {
                _unitOfWork.Repository<UserGroup>().Create(model);
                _unitOfWork.Save();
                return Ok(new {message = "Joined Group Succesfully"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpDelete("{groupId}")]
        public IActionResult LeaveGroup(Guid groupId, string userId)
        {
            try
            {
                var userFromGroup = _unitOfWork.Repository<UserGroup>().GetAll().Where(u => u.GroupId == groupId && u.UserId == userId).FirstOrDefault();
                _unitOfWork.Repository<UserGroup>().DeleteObject(userFromGroup);
                _unitOfWork.Save();
                return Ok(new { message = "Leave Group Succesfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



    }
}
