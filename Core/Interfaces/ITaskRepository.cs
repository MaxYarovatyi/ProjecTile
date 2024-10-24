using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ITaskRepository
    {
        Task<ProjectTask> GetTaskById(string id);
        Task<List<ProjectTask>> GetTasksAsync();
        Task<ProjectTask> UpdateTaskAsync(ProjectTask task);
    }
}