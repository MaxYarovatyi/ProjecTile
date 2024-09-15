using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> GetProjectById(int id);
        Task<IReadOnlyList<Project>> GetProjectsAsync();
        Task<IEnumerable<Core.Entities.TaskStatus>> GetTaskStatusesAsync();
    }
}