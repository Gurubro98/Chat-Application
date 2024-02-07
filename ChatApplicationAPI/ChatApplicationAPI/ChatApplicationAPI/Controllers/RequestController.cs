using AutoMapper;
using BAL.Services.MessageService;
using BAL.Services.UnitOfWork;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplicationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController, Authorize(AuthenticationSchemes ="Bearer")]
    public class RequestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RequestController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public IActionResult GetAllRequest(string userId) 
        {
            var requests = _unitOfWork.Repository<Request>().GetAll().Where(u => (u.ReceiverId == userId && !u.IsDeleted)|| (u.SenderId == userId && !u.IsDeleted)).ToList();
            return Ok(new { requests });
        }

        [HttpGet("{userId}")]
        public IActionResult GetNumberOfRequest(string userId)
        {
            var requests = _unitOfWork.Repository<Request>().GetAll().Where(u => (u.ReceiverId == userId && !u.IsDeleted) || (u.SenderId == userId && !u.IsDeleted)).ToList();
            return Ok(new { count = requests.Count() });
        }
    }
}
