using System;
using System.Collections.Generic;
using System.Linq;
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

        public ProjectController(IProjectRepository repository, UserManager<AccountUser> manager, ITaskRepository taskRepository)
        {
            _projectRepository = repository;
            _userManager = manager;
            _taskRepository = taskRepository;
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
    }
}