using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.VisualBasic;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class UserTasksRepository : IUserTaskRepository
    {
        private readonly IDatabase _database;

        public UserTasksRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase(4);
        }

        public async Task<UserTasks> GetUserTasks(string id)
        {
            var res = await _database.StringGetAsync(id);
            return res.IsNullOrEmpty ? null : JsonSerializer.Deserialize<UserTasks>(res);
        }
        public async Task<UserTasks> AddTask(string userId, string taskId)
        {
            var data = await _database.StringGetAsync(userId);
            if (data.IsNullOrEmpty) return null;
            var tasks = JsonSerializer.Deserialize<UserTasks>(data);
            tasks.Tasks.Add(taskId);
            var res = await _database.StringSetAsync(userId, JsonSerializer.Serialize(tasks));
            return res ? JsonSerializer.Deserialize<UserTasks>(await _database.StringGetAsync(userId)) : null;
        }
    }
}