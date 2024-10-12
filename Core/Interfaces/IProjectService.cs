using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProjectService
    {
        public Task<Project> CreateProjectAsync(string userEmail, Project project);
        public Task<IReadOnlyList<string>> GetProjectTasksById(string id);
    }
}