using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace API.Dtos
{
    public class ProjectDto
    {
        public string? Guid { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Name { get; set; }
        public UserDto Owner { get; set; }
        public List<UserDto> Participants { get; set; }
        public List<TaskDto>? Tasks { get; set; }
        public List<string> Comments { get; set; }
    }
}