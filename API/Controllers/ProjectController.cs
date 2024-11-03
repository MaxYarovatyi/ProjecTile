using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly UserManager<AccountUser> _userManager;
        private readonly ITaskRepository _taskRepository;
        private readonly IUserProjectsRepository _userProjectsRepository;

        public ProjectController(IProjectRepository repository, UserManager<AccountUser> manager, ITaskRepository taskRepository, IUserProjectsRepository userProjectsRepository = null)
        {
            _projectRepository = repository;
            _userManager = manager;
            _taskRepository = taskRepository;
            _userProjectsRepository = userProjectsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Project>>> Get()
        {
            return Ok(await _projectRepository.GetProjectsAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> Get(string id)
        {
            var res = await MappingHelper.GetProjectDtoByIdAsync(id, _projectRepository, _taskRepository, _userManager);
            return res;
        }
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> UpdateProject(ProjectDto project)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            var userProjects = await _userProjectsRepository.GetUserProjectsById(user.Id);
            userProjects.Projects.Add(project.Guid);
            var updated = await _userProjectsRepository.UpdateUserProjects(user.Id, userProjects);

            var convertedProject = MappingHelper.ConvertProjectDtoToProject(project);
            var res = await _projectRepository.UpdateProject(convertedProject);
            return res == null ? null : await Get(res.Guid);
        }
        [HttpGet("getTasks/{id}")]
        public async Task<ActionResult<List<TaskDto>>> MyMethodAsync(string id)
        {
            var project = await _projectRepository.GetProjectById(id);
            if (project == null) return NotFound("Project not found");
            var tasks = new List<TaskDto>();
            foreach (var taskId in project.Tasks)
            {
                var task = await MappingHelper.GetTaskAsync(taskId, _taskRepository, _userManager);
                tasks.Add(task);
            }
            return Ok(tasks);
        }
        [HttpPost("removeuser")]
        public async Task<ActionResult<ProjectDto>> RemoveUserFromProject([FromQuery] string projectId, [FromQuery] string userId)
        {
            var finded = await this._projectRepository.GetProjectById(projectId);
            if (finded == null) return NotFound("Project Not Found");
            if (!finded.Participants.Contains(userId)) return NotFound("User not found in project");
            finded.Participants.Remove(userId);
            foreach (var taskId in finded.Tasks)
            {
                var task = await this._taskRepository.GetTaskById(taskId);
                task.AssignedTo.Remove(userId);
                await this._taskRepository.UpdateTaskAsync(task);
            }
            var res = await _projectRepository.UpdateProject(finded);
            return res == null ? null : await MappingHelper.GetProjectDtoByIdAsync(res.Guid, this._projectRepository, this._taskRepository, this._userManager);
        }
        [HttpPost("adduser")]
        public async Task<ActionResult<ProjectDto>> AddUserFromProject([FromQuery] string projectId, [FromQuery] string userId)
        {
            var finded = await this._projectRepository.GetProjectById(projectId);
            if (finded == null) return NotFound("Project Not Found");
            if (finded.Participants.Contains(userId)) return BadRequest("User already exists"); ;
            finded.Participants.Add(userId);
            var res = await _projectRepository.UpdateProject(finded);
            return res == null ? null : await MappingHelper.GetProjectDtoByIdAsync(res.Guid, this._projectRepository, this._taskRepository, this._userManager);
        }
    }
}