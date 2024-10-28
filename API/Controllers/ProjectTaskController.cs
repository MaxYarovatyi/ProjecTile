using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
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
        private readonly UserManager<AccountUser> _manager;

        public ProjectTaskController(ITaskRepository taskRepository, UserManager<AccountUser> manager)
        {
            _taskRepository = taskRepository;
            _manager = manager;
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
            var res = await _taskRepository.UpdateTaskAsync(MappingHelper.ConvertTaskDtoToProjectTask(task));
            return res == null ? BadRequest() : Ok(await MappingHelper.GetTaskAsync(res.Guid, _taskRepository, _manager));
        }
    }
}