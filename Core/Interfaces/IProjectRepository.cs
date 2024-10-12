using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> GetProjectById(string id);
        Task<IReadOnlyList<Project>> GetProjectsAsync();
        Task<Project> UpdateProject(Project project);

    }
}