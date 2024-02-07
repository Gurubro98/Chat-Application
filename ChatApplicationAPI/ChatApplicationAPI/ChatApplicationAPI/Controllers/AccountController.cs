using AutoMapper;
using BAL.Services.UnitOfWork;
using DAL.ModelDTO;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatApplicationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        public readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AccountController(IMapper mapper, IWebHostEnvironment webHostEnvironment, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]

        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                List<string> excludedUserIds = _unitOfWork.Repository<MutualRelation>().GetAll().Where(m => m.MutualId == userId || m.UserId == userId).Select(m => m.UserId == userId ? m.MutualId : m.UserId).ToList();
                
                List<string> requestUserIds = _unitOfWork.Repository<Request>().GetAll().Where(m => m.SenderId == userId && !m.IsDeleted).Select(m => m.ReceiverId).ToList();

                var users = await _userManager.Users.Where(u => !excludedUserIds.Contains(u.Id)).ToListAsync();
                foreach(var user in users)
                {
                    if (requestUserIds.Contains(user.Id))
                    {
                        user.IsSendRequest = true;
                    }
                }
                //var users = _unitOfWork.Repository<UserGroup>().GetAll().Where(u => u.UserId == userId).ToList();
                return Ok(new { users });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{userId}")]

        public async Task<IActionResult> GetAllMutualUsers(string userId)
        {
            try
            {
                var users = await _unitOfWork.Repository<MutualRelation>().GetAll().Where(u => u.UserId == userId || u.MutualId == userId).ToListAsync();
                //var users = _unitOfWork.Repository<UserGroup>().GetAll().Where(u => u.UserId == userId).ToList();
                return Ok(new { users });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{userId}")]

        public async Task<IActionResult> GetAllLoginUser(string userId)
        {
            try
            {
                var currentUser = await _userManager.FindByIdAsync(userId);
                //var users = _unitOfWork.Repository<UserGroup>().GetAll().Where(u => u.UserId == userId).ToList();
                return Ok(new { currentUser });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromForm]Register registerModel)
        {
            try
            {
                var existuser = await _userManager.FindByEmailAsync(registerModel.Email);
                if (existuser != null)
                {
                    return BadRequest(new { message = "Email is already registerd" });
                }
                if (registerModel.ProfilePic != null && registerModel.ProfilePic.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Resources/images");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var filePath = Path.Combine(uploadsFolder, registerModel.ProfilePic.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await registerModel.ProfilePic.CopyToAsync(stream);
                    }

                    registerModel.ImageUrl = filePath;
                }

                var user = _mapper.Map<User>(registerModel);
                var result = await _userManager.CreateAsync(user, registerModel.Password);

                await _userManager.AddToRoleAsync(user, "User");
                return Ok(new { message = user.Name + "Registerd Successfully" });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { message = "Error: email is already registerd." });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)

        {
            try
            {
                User user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {

                    if (!await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        return Unauthorized(new { message = "Invalid Credentials" });
                    }
                    var accessToken = GenerateToken(user);


                    return Ok(new { message = "User Logged-in Successfully!", token = accessToken });
                }
                else
                {
                    return NotFound(new { message = "User does n't exist" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GenerateToken(User user)
        {
            List<string> roles = _userManager.GetRolesAsync(user).Result.ToList();
            var claims = new[] {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim("Name", user.Name),
            new Claim("Id", user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, roles[0]),
            new Claim("Role", roles[0]),
            new Claim(ClaimTypes.NameIdentifier,
            user.Id)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims,
                expires: DateTime.Now.AddDays(1), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
