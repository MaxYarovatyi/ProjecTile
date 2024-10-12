using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class TaskDto
    {
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
        public IReadOnlyList<UserDto> AssignedTo { get; set; }
        public IReadOnlyList<string> Comments { get; set; }
    }
}