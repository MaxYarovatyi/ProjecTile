using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AccountUser> _userManager;
        private readonly SignInManager<AccountUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IUserProjectsRepository _userProjectsRepository;
        private readonly IUserTaskRepository _taskRepo;
        public AccountController(UserManager<AccountUser> userManager, SignInManager<AccountUser> signInManager, ITokenService tokenService, IUserTaskRepository userTasksRepository, IUserProjectsRepository userProjectsRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userProjectsRepository = userProjectsRepository;
            _taskRepo = userTasksRepository;
        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null ? true : false;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }




        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized();
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized();

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value) return new BadRequestObjectResult(new object { });
            var user = new AccountUser
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new object { });
            await _userProjectsRepository.UpdateUserProjects(user.Id, null);
            await _taskRepo.UpdateUserTasks(user.Id, null);
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }
        [HttpGet("search")]
        public async Task<ActionResult<List<UserDto>>> SearchUsersByEmail([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return BadRequest("Email is required");
            var users = _userManager.Users.Where(e => e.Email.Contains(email)).Select(u => new UserDto
            {
                Id = u.Id,
                Email = u.Email,
                Token = "",
                DisplayName = u.DisplayName
            }).ToList();
            return users != null
            ? users
            : NotFound("User not found");
        }
    }
}