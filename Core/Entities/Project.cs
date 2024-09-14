using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Project : BaseEntity
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Name { get; set; }
        public string OwnerId { get; set; }
        public List<AccountUser> Participants { get; set; }
        public List<Task> Tasks { get; set; }
        public List<string> Comments { get; set; }

    }
}