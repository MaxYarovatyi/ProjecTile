using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Project
    {
        public string Guid { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public List<string> Participants { get; set; }
        public List<string> Tasks { get; set; }
        public List<string> Comments { get; set; }

    }
}