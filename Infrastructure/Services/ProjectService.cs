using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserProjectsRepository _userProjectRepository;
        private readonly UserManager<AccountUser> _manager;

        public ProjectService(IProjectRepository projectRepository, IUserProjectsRepository userProjectRepository, UserManager<AccountUser> manager)
        {
            _projectRepository = projectRepository;
            _userProjectRepository = userProjectRepository;
            _manager = manager;
        }

        public async Task<Project> CreateProjectAsync(string userEmail, Project project)
        {
            var user = await _manager.FindByEmailAsync(userEmail);
            var newProject = new Project
            {
                Guid = project.Guid,
                Description = project.Description,
                Status = project.Status,
                CreatedAt = project.CreatedAt,
                Participants = project.Participants,
                Tasks = [],
                OwnerId = user.Id,
                Comments = []
            };
            return await _projectRepository.UpdateProject(newProject);
        }




        public async Task<IReadOnlyList<string>> GetProjectTasksById(string id)
        {
            var project = await _projectRepository.GetProjectById(id);
            return project.Tasks;
        }
    }
}