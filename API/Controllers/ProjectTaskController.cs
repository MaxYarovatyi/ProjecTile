using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserTaskRepository _userTaskRepository;
        private readonly UserManager<AccountUser> _manager;

        public ProjectTaskController(ITaskRepository taskRepository, UserManager<AccountUser> manager, IUserTaskRepository userTaskRepository = null)
        {
            _taskRepository = taskRepository;
            _manager = manager;
            _userTaskRepository = userTaskRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTaskById(string id)
        {
            var task = await MappingHelper.GetTaskAsync(id, _taskRepository, _manager);
            return task != null ? task : NotFound("Task not found");
        }
        [HttpPost("UpdateStatus/{id}")]
        public async Task<ActionResult<TaskDto>> UpdateTaskStatus(string id, [FromQuery] string status)
        {
            var task = await _taskRepository.GetTaskById(id);
            if (task == null) return NotFound("Task is not found!");
            task.Status = status;
            await _taskRepository.UpdateTaskAsync(task);
            return await MappingHelper.GetTaskAsync(id, _taskRepository, _manager);

        }
        [HttpPost]
        public async Task<ActionResult<TaskDto>> UpdateTask(TaskDto task)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _manager.FindByEmailAsync(email);
            var update = await _userTaskRepository.AddTask(user.Id, task.Guid);
            if (update == null) return NotFound("User tasks updating problem");
            var res = await _taskRepository.UpdateTaskAsync(MappingHelper.ConvertTaskDtoToProjectTask(task));
            return res == null ? BadRequest() : Ok(await MappingHelper.GetTaskAsync(res.Guid, _taskRepository, _manager));
        }
        [HttpGet("user/{id}")]
        public async Task<ActionResult<List<TaskDto>>> GetTasksForUsers(string id)
        {
            var tasks = await _userTaskRepository.GetUserTasks(id);
            if (tasks == null) return NotFound("Tasks not found");
            var tasksToReturn = new List<TaskDto>();
            foreach (var taskId in tasks.Tasks)
            {
                var data = await MappingHelper.GetTaskAsync(taskId, _taskRepository, _manager);
                tasksToReturn.Add(data);
            }
            return tasksToReturn;
        }
        // [HttpPost("user/{id}")]
        // public async Task<ActionResult<List<TaskDto>>> UpdateTasksForUser(string userId, )
        // {
            
        // }
    }
}