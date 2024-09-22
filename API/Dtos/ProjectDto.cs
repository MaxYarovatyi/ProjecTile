using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProjectDto
    {
         public string? Guid { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Name { get; set; }
        public string OwnerId { get; set; }
        public List<string> ParticipantsGuids { get; set; }
        public List<string> TasksGuids { get; set; }
        public List<string> Guids { get; set; }
    }
}