using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserTaskRepository
    {
        public Task<UserTasks> GetUserTasks(string id);
        public Task<UserTasks> AddTask(string userId, string taskId);
        public Task<UserTasks> UpdateUserTasks(string userId, UserTasks tasks);
    }
}