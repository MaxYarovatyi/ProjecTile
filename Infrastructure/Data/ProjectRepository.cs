using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class ProjectRepository(StoreContext context) : IProjectRepository
    {
        private StoreContext _context = context;

        public async Task<Project> GetProjectById(int id)
        {
            return await _context.Projects.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Project>> GetProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public Task<IEnumerable<Core.Entities.TaskStatus>> GetTaskStatusesAsync()
        {
            return null;
        }

    }
}