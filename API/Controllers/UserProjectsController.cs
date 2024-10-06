using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProjectsController : ControllerBase
    {
        private readonly IUserProjectsRepository _repository;
        private readonly UserManager<AccountUser> _userManager;



        public UserProjectsController(UserManager<AccountUser> manager, IUserProjectsRepository repository)
        {
            _repository = repository;
            _userManager = manager;

        }
        [HttpGet]
        public async Task<ActionResult<UserProjects>> GetCurrentUserProjects()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = _userManager.FindByEmailAsync(email);
            return await _repository.GetUserProjectsById(user.Result.Id.ToString());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProjects>> GetUserProjectsById(string id)
        {
            var res = await _repository.GetUserProjectsById(id);
            return res ?? null;
        }
    }
}