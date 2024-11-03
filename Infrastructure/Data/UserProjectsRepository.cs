using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class UserProjectsRepository : IUserProjectsRepository
    {
        private readonly StackExchange.Redis.IDatabase _database;
        public UserProjectsRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase(3);
        }
        public async Task<UserProjects> GetUserProjectsById(string id)
        {
            var data = await _database.StringGetAsync(id);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<UserProjects>(data);
        }
        public async Task<UserProjects> UpdateUserProjects(string id, UserProjects projects)
        {
            var data = await GetUserProjectsById(id);
            var updatedProjects = data == null ? new UserProjects
            {
                Id = id,
                Projects = []
            } : projects;
            var created = await _database.StringSetAsync(id, JsonSerializer.Serialize(updatedProjects));
            return !created ? null : await GetUserProjectsById(id);
        }

    }
}