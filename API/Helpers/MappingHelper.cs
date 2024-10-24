using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace API.Helpers
{
    public static class MappingHelper
    {
        public static async Task<UserDto> GetUserAsync(string id, UserManager<AccountUser> manager)
        {
            var user = await manager.FindByIdAsync(id);
            return user == null ? null : new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = "",
                DisplayName = user.DisplayName
            };
        }
        public static async Task<TaskDto> GetTaskAsync(string id, ITaskRepository repo, UserManager<AccountUser> manager)
        {
            var task = await repo.GetTaskById(id);
            var participants = new List<UserDto>();
            foreach (var user in task.AssignedTo)
            {
                var findedUser = await MappingHelper.GetUserAsync(user, manager);
                participants.Add(findedUser);
            }
            return task == null ? null : new TaskDto
            {
                Guid = task.Guid,
                Title = task.Title,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                DueDate = task.DueDate,
                Status = task.Status,
                Comments = task.Comments,
                AssignedTo = participants
            };
        }
        public static async Task<ProjectDto> GetProjectDtoByIdAsync(string id, IProjectRepository projectRepository, ITaskRepository repository, UserManager<AccountUser> manager)
        {
            var project = await projectRepository.GetProjectById(id);
            var owner = await MappingHelper.GetUserAsync(project.OwnerId, manager);
            var participants = new List<UserDto>();
            var tasks = new List<TaskDto>();
            foreach (var participant in project.Participants)
            {
                var user = await MappingHelper.GetUserAsync(participant, manager);
                participants.Add(user);
            }
            foreach (var task in project.Tasks)
            {
                var findedTask = await MappingHelper.GetTaskAsync(task, repository, manager);
                tasks.Add(findedTask);
            }
            return new ProjectDto
            {
                Guid = project.Guid,
                Name = project.Name,
                Description = project.Description,
                Owner = owner,
                Comments = project.Comments, ///!!!!!!!!!!!!!
                CreatedAt = project.CreatedAt,
                Tasks = tasks,
                Participants = participants,
                Status = project.Status
            };
        }
        public static Project ConvertProjectDtoToProject(ProjectDto dto)
        {
            return new Project
            {
                Guid = dto.Guid,
                Name = dto.Name,
                Description = dto.Description,
                Status = dto.Status,
                OwnerId = dto.Owner.Id,
                CreatedAt = dto.CreatedAt,
                Participants = dto.Participants.Select(p => p.Id).ToList(),
                Tasks = dto.Tasks.Select(t => t.Guid).ToList(),
                Comments = dto.Comments
            };
        }
    }
}