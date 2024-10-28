using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class TaskRepository : ITaskRepository
    {

        private readonly StackExchange.Redis.IDatabase _database;

        public TaskRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase(1);
        }

        public async Task<ProjectTask> GetTaskById(string id)
        {
            var data = await _database.StringGetAsync(id);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ProjectTask>(data, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<ProjectTask>> GetTasksAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ProjectTask> UpdateTaskAsync(ProjectTask task)
        {
            var res = await _database.StringSetAsync(task.Guid, JsonSerializer.Serialize(task));
            return !res ? null : JsonSerializer.Deserialize<ProjectTask>(await _database.StringGetAsync(task.Guid),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
            );
        }
    }
}