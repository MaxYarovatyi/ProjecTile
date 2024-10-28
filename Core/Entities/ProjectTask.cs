using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Macross.Json.Extensions;

namespace Core.Entities
{

    public class ProjectTask
    {
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
        public IReadOnlyList<string> AssignedTo { get; set; }
        public IReadOnlyList<string> Comments { get; set; }
        //public IReadOnlyList<string> AttachedFiles { get; set; }
    }
}