using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
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
    [Authorize]
    public class UserProjectsController : ControllerBase
    {

        private readonly IUserProjectsRepository _repository;
        private readonly UserManager<AccountUser> _userManager;
        private readonly IProjectService _projectService;
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;

        public UserProjectsController(IUserProjectsRepository repository, UserManager<AccountUser> userManager, IProjectService projectService, IProjectRepository projectRepository, ITaskRepository taskRepository)
        {
            _repository = repository;
            _userManager = userManager;
            _projectService = projectService;
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProjectDto>>> GetCurrentUserProjects()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            var data = await _repository.GetUserProjectsById(user.Id);
            var projectsToReturn = new List<ProjectDto>();
            foreach (var project in data.Projects)
            {
                var findedProject = await MappingHelper.GetProjectDtoByIdAsync(project, _projectRepository, _taskRepository, _userManager);
                projectsToReturn.Add(findedProject);
            }
            return Ok(projectsToReturn);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<Project>>> GetUserProjectsById(string id)
        {
            var res = await _projectService.GetProjectTasksById(id);
            return Ok(res) ?? null;
        }
    }
}