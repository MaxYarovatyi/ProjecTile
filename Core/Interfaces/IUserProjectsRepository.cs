using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserProjectsRepository
    {
        public Task<UserProjects> GetUserProjectsById(string id);
        public Task<UserProjects> UpdateUserProjects(string id, UserProjects projects);
    }
}