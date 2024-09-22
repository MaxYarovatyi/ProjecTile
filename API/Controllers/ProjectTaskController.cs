using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public ProjectTaskController(ITaskRepository repository)
        {
            _taskRepository = repository;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTask>> GetTaskById(string id)
        {
            return Ok(await _taskRepository.GetTaskById(id));
        }
        [HttpGet]
        public async Task<ActionResult<List<ProjectTask>>> GetTasksAsync()
        {
            return Ok(await _taskRepository.GetTasksAsync());
        }
    }
}