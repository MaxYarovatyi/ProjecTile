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
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
        public async Task<ActionResult<IReadOnlyList<ProjectDto>>> GetUserProjectsId(string id)
        {

            var data = await _repository.GetUserProjectsById(id);
            var projects = data.Projects;
            var projectsToReturn = new List<ProjectDto>();
            foreach (var project in projects)
            {
                var findedProject = await MappingHelper.GetProjectDtoByIdAsync(project, _projectRepository, _taskRepository, _userManager);
                projectsToReturn.Add(findedProject);
            }
            return projectsToReturn;
        }
        [HttpPost]
        public async Task<ActionResult<IReadOnlyList<ProjectDto>>> UpdateUserProjects(string id, string projectId)
        {
            var data = await _repository.GetUserProjectsById(id);
            var prjects = data == null ? [] : data.Projects;
            prjects.Add(projectId);
            var result = await _repository.UpdateUserProjects(id, new UserProjects
            {
                Projects = prjects,
                Id = id
            });
            return await GetUserProjectsId(id);
        }
    }
}