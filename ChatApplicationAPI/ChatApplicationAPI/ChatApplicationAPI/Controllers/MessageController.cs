using AutoMapper;
using BAL.Services.MessageService;
using BAL.Services.UnitOfWork;
using DAL.ModelDTO;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatApplicationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController, Authorize(AuthenticationSchemes = "Bearer")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MessageController(IMessageService messageService, IMapper mapper, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _messageService = messageService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetAllUnReadMessages()
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var unReadMessages = _messageService.GetAllUnReadMessages(userId);
                if (unReadMessages == null)
                {
                    return NotFound(new { message = "No UnRead Message" });
                }
                return Ok(new { unReadMessages });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllMessages(string senderId, string receiverId, int page = 1)
        {
            try
            {
                var messages = _messageService.GetAllMessages(senderId, receiverId, page);
                if (messages == null)
                {
                    return NotFound(new { message = "Messagaes is Empty" });
                }
                return Ok(new { messages });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllGroupMessages(Guid groupId, int page = 1)
        {
            try
            {
                var messages = _messageService.GetAllGroupMessages(groupId, page);
                if (messages == null)
                {
                    return NotFound(new { message = "Messagaes is Empty" });
                }
                return Ok(new { messages });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAttachment([FromForm]Attachment attachmentModel)
        {
            try
            {
                if (attachmentModel.AttachmentFile != null && attachmentModel.AttachmentFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Resources/images");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var filePath = Path.Combine(uploadsFolder, attachmentModel.AttachmentFile.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await attachmentModel.AttachmentFile.CopyToAsync(stream);
                    }

                    attachmentModel.FileName = filePath;
                }

                var msgDate = DateTime.Parse(attachmentModel.createdTime.ToString());
                attachmentModel.createdTime = msgDate.ToUniversalTime();
                _unitOfWork.Repository<Attachment>().Create(attachmentModel);
                _unitOfWork.Save();

                var attachment = _unitOfWork.Repository<Attachment>().GetAll().OrderByDescending(c => c.createdTime).FirstOrDefault();
                if (attachment == null)
                {
                    return NotFound(new { message = "attachment not Found" });
                }
                return Ok(new { attachment });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(MessageDTO messageModel)
        {
            var message = _mapper.Map<Message>(messageModel);
            _messageService.Add(message);
            return Ok(message);
        }

        [HttpPut]
        public async Task<IActionResult> MessageSeen(MessageDTO messageModel)
        {
            var message = _mapper.Map<Message>(messageModel);
            _messageService.MessageSeen(message);
            return Ok(new { message = "Message Seen Successfully" });
        }
    }
}
