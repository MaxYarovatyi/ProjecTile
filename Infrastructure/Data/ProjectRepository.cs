using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly StackExchange.Redis.IDatabase _database;

        public ProjectRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<Project> GetProjectById(string id)
        {
            var data = await _database.StringGetAsync(id);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Project>(data, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<IReadOnlyList<Project>> GetProjectsAsync()
        {
            var data = await _database.StringGetAsync("");
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<List<Project>>(data);
        }

        public async Task<Project> UpdateProject(Project project)
        {
            var created = await _database.StringSetAsync(project.Guid, JsonSerializer.Serialize(project));
            return created ? await GetProjectById(project.Guid) : null;
        }

    }
}