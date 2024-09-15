using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Project : BaseEntity
    {
        public string Guid { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Name { get; set; }
        public string OwnerId { get; set; }
        public List<AccountUser> Participants { get; set; } = new List<AccountUser>();
        public List<string> ParticipantsId { get; set; } = new List<string>();
        public List<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
        public List<string> Comments { get; set; } = new List<string>();

    }
}